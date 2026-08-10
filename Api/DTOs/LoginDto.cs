using System.ComponentModel.DataAnnotations;

namespace Tpo_DotNet_bb.Api.DTOs;

public class LoginDto
{
    [Required]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
