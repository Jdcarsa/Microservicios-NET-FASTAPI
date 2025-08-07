

using Microsoft.EntityFrameworkCore;
using Application.Model.userModel.model;
using Microsoft.AspNetCore.Identity;

namespace User.Service.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                FullName = "Administrador",
                Email = "admin@curso.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                PhoneNumber = "600123456",
                Role = "Admin"
            });
            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                FullName = "Usuario Normal",
                Email = "user@curso.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                PhoneNumber = "600987654",
                Role = "User"
            });


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
