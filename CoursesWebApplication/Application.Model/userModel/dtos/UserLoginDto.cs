
using System.ComponentModel.DataAnnotations;

namespace Application.Model.userModel.dtos
{
    public class UserLoginDto
    {
            [Required(ErrorMessage = "El correo es obligatorio")]
            [EmailAddress(ErrorMessage = "Correo inválido")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "La contraseña es obligatoria")]
            public string Password { get; set; } = string.Empty;
    }
}
