using System.ComponentModel.DataAnnotations;

public class ClienteRegistroDto
{
    [EmailAddress]
    public string Email { get; set; } = "";

    public string Apellido { get; set; } = "";

    public string Nombre { get; set; } = "";

    public string Direccion { get; set; } = "";

    public string Password { get; set; } = "";
}
