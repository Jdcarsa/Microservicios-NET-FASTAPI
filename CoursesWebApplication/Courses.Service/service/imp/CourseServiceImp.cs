using Application.Model.courseModel.dto;
using Application.Model.courseModel.model;
using Courses.Service.data;
using Courses.Service.kafka;
using Courses.Service.service.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Courses.Service.service.imp
{
    public class CourseServiceImp : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly KafkaProducer _kafka;

        public CourseServiceImp(AppDbContext context, IWebHostEnvironment env, KafkaProducer kafka)
        {
            _context = context;
            _env = env;
            _kafka = kafka;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            return await _context.Courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Duration = c.Duration,
                CreatedAt = c.CreatedAt,
                Price = c.Price,
                ImagePath = c.ImagePath
            }).ToListAsync();
        }

        public async Task<CourseDto?> GetByIdAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return null;
            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration,
                CreatedAt = course.CreatedAt,
                Price = course.Price,
                ImagePath = course.ImagePath
            };
        }

        public async Task<CourseDto> CreateAsync(CourseCUDto dto, IFormFile? image)
        {
            string? imagePath = null;
            if (image != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(_env.WebRootPath, "images/courses", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                imagePath = $"images/courses/{fileName}";
            }

            var course = new CourseModel
            {
                Name = dto.Name,
                Description = dto.Description,
                Duration = dto.Duration,
                Price = dto.Price,
                ImagePath = imagePath
            };

            _context.Courses.Add(course);

            await _context.SaveChangesAsync();

            await _kafka.SendMessage("CREATED", new
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Price = course.Price,
                Timestamp = DateTime.UtcNow
            });

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration,
                CreatedAt = course.CreatedAt,
                Price = course.Price,
                ImagePath = course.ImagePath
            };
        }

        public async Task<bool> UpdateAsync(Guid id, CourseCUDto dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;
            course.Name = dto.Name;
            course.Description = dto.Description;
            course.Duration = dto.Duration;
            course.Price = dto.Price;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CourseDto> GetByNameAsync(string name)
        {
            var course = await _context.Courses
                .Where(c => c.Name.ToLower() == name.ToLower())
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Duration = c.Duration,
                    CreatedAt = c.CreatedAt,
                    Price = c.Price,
                    ImagePath = c.ImagePath
                })
                .FirstOrDefaultAsync();

            if (course == null)
                throw new KeyNotFoundException($"Course with name '{name}' not found.");

            return course;
        }
    }
}
