using Application.Model.userModel.dtos;
using Application.Model.userModel.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Service.data;
using User.Service.service.interfaces;
using User.Service.kafka;

namespace User.Service.service.Imp
{
    public class UserServiceImp : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserServiceImp(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        public async Task<UserDto?> GetUserById(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user == null ? null : new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }


        public async Task<bool> UpdateUserAsync(Guid id, UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<List<UserDto>> GetAllUsers()
        {
            var users = _context.Users.Select(u => new UserDto
            {
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).ToListAsync();
            if (users == null) return Task.FromResult(new List<UserDto>());
            return users;
        }

        public Task<UserDto?> GetUserByEmail(string email)
        {
            var user = _context.Users
                .Where(u => u.Email == email)
                .Select(u => new UserDto
                {
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                })
                .FirstOrDefaultAsync();
            if (user == null) return Task.FromResult<UserDto?>(null);
            return user;
        }
    }
}
