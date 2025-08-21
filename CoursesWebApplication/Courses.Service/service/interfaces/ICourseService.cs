using Application.Model.courseModel.dto;

namespace Courses.Service.service.interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(Guid id);
        Task<CourseDto> CreateAsync(CourseCUDto dto);
        Task<bool> UpdateAsync(Guid id, CourseCUDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<CourseDto> GetByNameAsync(string name);
    }
}
