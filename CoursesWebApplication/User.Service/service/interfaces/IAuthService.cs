using Application.Model.userModel.dtos;

namespace User.Service.service.interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegisterDto dto);
        Task<string?> LoginAsync(UserLoginDto dto);
        Task<string> getTokenByEmail(string email);
    }
}
