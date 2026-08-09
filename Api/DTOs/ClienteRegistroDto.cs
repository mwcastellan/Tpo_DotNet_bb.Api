using System.ComponentModel.DataAnnotations;

public class ClienteRegistroDto
{
    [Required]
    [EmailAddress(ErrorMessage = "EMAIL es incorrecto")]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z0-9]+$",
        ErrorMessage = "APELLIDO es incorrecto")]
    public string Apellido { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z0-9]+$",
        ErrorMessage = "NOMBRE es incorrecto")]
    public string Nombre { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z0-9\s,.'-]{3,}$",
        ErrorMessage = "DIRECCION es incorrecta")]
    public string Direccion { get; set; }

    [Required]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "PASSWORD es incorrecto")]
    public string Password { get; set; }
}
