using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Application.Model.userModel.model
{
    [Table("Users")]
    public class UserModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; } = "User";
    }
}
