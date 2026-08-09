using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tpo_DotNet_bb.Api.Api.DTOs;
using Tpo_DotNet_bb.Api.Api.Entities;

namespace Tpo_DotNet_bb.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly Entities.AppDbContext _context;
    private readonly IConfiguration _configuration;

    public ClientesController(
        Entities.AppDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // GET api/clientes
    [HttpGet]
    public async Task<IActionResult> GetClientes()
    {
        var clientes = await _context.Clientes
            .Select(x => new
            {
                x.ID,
                x.EMAIL,
                x.APELLIDO,
                x.NOMBRE,
                x.DIRECCION
            })
            .OrderBy(x => x.ID)
            .ToListAsync();

        return Ok(clientes);
    }

    // GET api/clientes/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCliente(int id)
    {
        var cliente = await _context.Clientes
            .Where(x => x.ID == id)
            .Select(x => new
            {
                x.ID,
                x.EMAIL,
                x.APELLIDO,
                x.NOMBRE,
                x.DIRECCION
            })
            .FirstOrDefaultAsync();

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    // POST api/clientes/registrar
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(
        ClienteRegistroDto dto)
    {
        if (await _context.Clientes
            .AnyAsync(x => x.EMAIL == dto.Email))
        {
            return BadRequest(
                new { mensaje = "El email ya existe" });
        }

        var cliente = new Clientes
        {
            EMAIL = dto.Email,
            APELLIDO = dto.Apellido,
            NOMBRE = dto.Nombre,
            DIRECCION = dto.Direccion,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            PASSWORD = BCrypt.Net.BCrypt.HashPassword(
            dto.Password)
        };

        _context.Clientes.Add(cliente);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje =
                "Cliente registrado correctamente"
        });
    }

    // POST api/clientes/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(
                x => x.EMAIL == dto.Email);

        if (cliente == null)
        {
            return BadRequest(
                new
                {
                    mensaje =
                        "Cliente o contraseña incorrecta"
                });
        }

        var hasher = new PasswordHasher<Clientes>();
        bool passwordValida = BCrypt.Net.BCrypt.Verify(
                                                    dto.Password,
                                                    cliente.PASSWORD
                                                     );
        if (!passwordValida)
        {
            return BadRequest(
                new
                {
                    mensaje = "Cliente o contraseña incorrecta"
                });
        }

        var token = GenerarToken(cliente);
        // Envio Cookie
        Response.Cookies.Append(
            "tpo_dotnet_bb",
            token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires =
                    DateTimeOffset.UtcNow.AddHours(1)
            });

        return Ok(new
        {
            mensaje = "Login correcto",
            token
        });
    }

    // PUT api/clientes/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        ClienteUpdateDto dto)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
            return NotFound();

        cliente.EMAIL = dto.Email;
        cliente.APELLIDO = dto.Apellido;
        cliente.NOMBRE = dto.Nombre;
        cliente.DIRECCION = dto.Direccion;
        cliente.UpdatedAt = DateTime.Now;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            var hasher = new PasswordHasher<Clientes>();

            cliente.PASSWORD = hasher.HashPassword(cliente, dto.Password);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje =
                "Cliente actualizado correctamente"
        });
    } // ← cerrar el método Actualizar

    private string GenerarToken(Clientes cliente)
    {
        // JWT
        var JWT_key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
        var JWT_time = int.Parse(_configuration["Jwt:Time"]!);
        var claims = new[]
        {
            new Claim("IDCLIENTE", cliente.ID.ToString()),
            new Claim("EMAIL", cliente.EMAIL)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(JWT_time),
            SigningCredentials = new SigningCredentials(
      new SymmetricSecurityKey(JWT_key),
      SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}