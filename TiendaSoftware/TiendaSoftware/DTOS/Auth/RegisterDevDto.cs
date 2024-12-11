using System.ComponentModel.DataAnnotations;

namespace TiendaSoftware.DTOS.Auth
{
    public class RegisterDevDto
    {
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "EL campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es valido.")]
        public string Email { get; set; }


        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "EL campo {0} es requerido.")]
        public string UserName { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(250)]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        public string Description { get; set; }

        [Display(Name = "Datos bancarios")]
        [StringLength(12)]
        [MinLength(12, ErrorMessage = "Los {0} debe tener al menos {1} caracteres.")]
        [Required(ErrorMessage = "Los {0} de la categoría es requerido.")]

        public string Securitycode { get; set; }



        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        // 1 mayuscula, 1 minuscula, 1 caracter especial, 1 numero, sea mayor a 8 caracteres
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "La contraseña debe ser segura y contener al menos 8 caracteres, incluyendo minúsculas, mayúsculas, números y caracteres especiales.")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


    }
}
