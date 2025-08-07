using Application.Model.courseModel.dto;
using Application.Model.courseModel.model;

namespace Courses.Service.service.interfaces
{
    public interface IPurchaseService
    {
        Task<bool> PurchaseCourseAsync(PurchaseDto dto);
        Task<List<PurchaseModel>> GetAllPurchasesAsync();
    }

}
