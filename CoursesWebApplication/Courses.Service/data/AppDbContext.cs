using Microsoft.EntityFrameworkCore;
using Application.Model.courseModel.model;
using System;

namespace Courses.Service.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding de cursos
            modelBuilder.Entity<CourseModel>().HasData(
                new CourseModel
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Curso de Programación",
                    Description = "Curso completo de programación desde cero.",
                    Duration = 40,
                    Price = 199.99,
                    CreatedAt = DateTime.UtcNow,
                    ImagePath = "images/courses/programacion.jpg"
                },
                new CourseModel
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Curso de Bases de Datos",
                    Description = "Curso completo sobre bases de datos relacionales y no relacionales.",
                    Duration = 30,
                    Price = 149.99,
                    CreatedAt = DateTime.UtcNow,
                    ImagePath = "images/courses/bases_de_datos.png"
                },
                new CourseModel
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "Curso de Desarrollo Web",
                    Description = "Curso de desarrollo web con HTML, CSS y JavaScript.",
                    Duration = 25,
                    Price = 99.99,
                    CreatedAt = DateTime.UtcNow,
                    ImagePath = "images/courses/desarrollo_web.png"
                }
            );

            // Seeding de compras (sin propiedades de navegación)
            modelBuilder.Entity<PurchaseModel>().HasData(
                new PurchaseModel
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CourseId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    PurchasedAt = DateTime.UtcNow,
                    Price = 199.99
                },
                new PurchaseModel
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CourseId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    PurchasedAt = DateTime.UtcNow,
                    Price = 149.99
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<PurchaseModel> Purchases
        {
            get; set;
        }
    }
}