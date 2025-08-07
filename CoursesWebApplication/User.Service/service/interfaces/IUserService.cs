using Application.Model.userModel.dtos;

namespace User.Service.service.interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserById(Guid id);
        Task<bool> UpdateUserAsync(Guid id, UserUpdateDto dto);
        Task<bool> DeleteUserAsync(Guid id);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto?> GetUserByEmail(string email);
    }
}
