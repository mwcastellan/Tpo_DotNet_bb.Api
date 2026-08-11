using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tpo_DotNet_bb.Api.DTOs;
using Tpo_DotNet_bb.Api.Entities;

namespace Tpo_DotNet_bb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : BaseController
{
    public ClientesController(
    Entities.AppDbContext context,
    IConfiguration configuration)
        : base(context, configuration)
    {
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

    // GET api/clientes/
    [HttpGet("cliente")]
    public async Task<IActionResult> GetCliente()
    {
        int idCliente = ObtenerIdCliente();
        var cliente = await _context.Clientes
            .Where(x => x.ID == idCliente)
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
        ClienteDto dto)
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
         ClienteDto dto)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
            return NotFound();

        cliente.EMAIL = dto.Email;
        cliente.APELLIDO = dto.Apellido;
        cliente.NOMBRE = dto.Nombre;
        cliente.DIRECCION = dto.Direccion;
        cliente.UpdatedAt = DateTime.Now;
        cliente.PASSWORD = BCrypt.Net.BCrypt.HashPassword(
                   dto.Password);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje =
                "Cliente actualizado correctamente"
        });
    } // ← cerrar el método Actualizar


}