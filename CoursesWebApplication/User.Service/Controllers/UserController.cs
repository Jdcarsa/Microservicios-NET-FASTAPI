using Application.Model.userModel.dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Service.service.interfaces;

namespace User.Service.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> Update(Guid id, UserUpdateDto dto)
        {
            var updated = await _userService.UpdateUserAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
