using System.ComponentModel.DataAnnotations;

namespace Tpo_DotNet_bb.Api.Api.DTOs
{
    public class ClienteRegistroDto
    {
        [Required(ErrorMessage = "EMAIL es obligatorio")]
        [EmailAddress(ErrorMessage = "EMAIL es incorrecto")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "APELLIDO es obligatorio")]
        [RegularExpression(
            @"^[a-zA-Z0-9]+$",
            ErrorMessage = "APELLIDO es incorrecto")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "NOMBRE es obligatorio")]
        [RegularExpression(
            @"^[a-zA-Z0-9]+$",
            ErrorMessage = "NOMBRE es incorrecto")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "DIRECCION es obligatoria")]
        [RegularExpression(
            @"^[a-zA-Z0-9\s,.'-]{3,}$",
            ErrorMessage = "DIRECCION es incorrecta")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "PASSWORD es obligatorio")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "PASSWORD es incorrecto")]
        public string Password { get; set; } = string.Empty;
    }
}