using Application.Model.userModel.dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Service.service.interfaces;

namespace User.Service.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var token = await _authService.RegisterAsync(dto);
            return Ok(new { Token = token }); ;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (string.IsNullOrEmpty(token)) return Unauthorized();
            return Ok(new { Token = token });
        }

        [HttpGet("token/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTokenByEmail(string email)
        {
            var token = await _authService.getTokenByEmail(email);
            if (string.IsNullOrEmpty(token)) return NotFound();
            return Ok(new { Token = token });
        }
    }
}
