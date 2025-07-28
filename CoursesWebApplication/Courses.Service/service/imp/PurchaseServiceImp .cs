using Application.Model.courseModel.dto;
using Application.Model.courseModel.model;
using Courses.Service.data;
using Courses.Service.kafka;
using Courses.Service.service.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Courses.Service.service.imp
{
    public class PurchaseServiceImp : IPurchaseService
    {
        private readonly AppDbContext _context;
        private readonly KafkaProducer _kafka;

        public PurchaseServiceImp(AppDbContext context, KafkaProducer kafka)
        {
            _context = context;
            _kafka = kafka;
        }

        public async Task<bool> PurchaseCourseAsync(PurchaseDto dto)
        {
            var course = await _context.Courses.FindAsync(dto.CourseId);
            if (course == null) return false;

            var purchase = new PurchaseModel
            {
                CourseId = dto.CourseId,
                UserId = dto.UserId,
                Price = dto.Price,
                PurchasedAt = dto.PurchasedAt
                
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            await _kafka.SendMessage("PURCHASED", new
            {
                CourseId = dto.CourseId,
                CourseName = course.Name,
                UserId = dto.UserId,
                Price = course.Price,
            }, topic: "course_events");


            return true;
        }

        public async Task<List<PurchaseModel>> GetAllPurchasesAsync()
        {
            return await _context.Purchases.Include(p => p.Course).ToListAsync();
        }
    }
}
