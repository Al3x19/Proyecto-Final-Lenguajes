using System.ComponentModel.DataAnnotations;

namespace TiendaSoftware.DTOS.Auth
{
    public class LoginDto
    {
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es obligatoria")]
        public string Password { get; set; }
    }
}
