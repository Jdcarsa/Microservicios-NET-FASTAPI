using Application.Model.courseModel.dto;
using Courses.Service.service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Service.Controllers
{
    [ApiController]
    [Route("api/v1/courses/purchases")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Purchase([FromBody] PurchaseDto dto)
        {
            var result = await _purchaseService.PurchaseCourseAsync(dto);
            return result ? Ok("Compra realizada") : NotFound("Curso no encontrado");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var purchases = await _purchaseService.GetAllPurchasesAsync();
            return Ok(purchases);
        }
    }

}
