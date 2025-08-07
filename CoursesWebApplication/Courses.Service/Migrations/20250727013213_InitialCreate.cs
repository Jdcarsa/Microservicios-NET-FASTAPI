using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Courses.Service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "Duration", "ImagePath", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 7, 27, 1, 32, 13, 214, DateTimeKind.Utc).AddTicks(6041), "Curso completo de programación desde cero.", 40, "images/courses/programacion.jpg", "Curso de Programación", 199.99000000000001 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 7, 27, 1, 32, 13, 214, DateTimeKind.Utc).AddTicks(6046), "Curso completo sobre bases de datos relacionales y no relacionales.", 30, "images/courses/bases_de_datos.png", "Curso de Bases de Datos", 149.99000000000001 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 7, 27, 1, 32, 13, 214, DateTimeKind.Utc).AddTicks(6050), "Curso de desarrollo web con HTML, CSS y JavaScript.", 25, "images/courses/desarrollo_web.png", "Curso de Desarrollo Web", 99.989999999999995 }
                });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Id", "CourseId", "Price", "PurchasedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("22222222-2222-2222-2222-222222222222"), 199.99000000000001, new DateTime(2025, 7, 27, 1, 32, 13, 214, DateTimeKind.Utc).AddTicks(6247), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("33333333-3333-3333-3333-333333333333"), 149.99000000000001, new DateTime(2025, 7, 27, 1, 32, 13, 214, DateTimeKind.Utc).AddTicks(6255), new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CourseId",
                table: "Purchases",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
