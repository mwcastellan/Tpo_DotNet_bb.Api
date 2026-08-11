using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tpo_DotNet_bb.Api.Entities;

namespace Tpo_DotNet_bb.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase

    {
        protected readonly Entities.AppDbContext _context;
        protected readonly IConfiguration _configuration;

        // Constructor protegido para inyección de dependencias (context + configuration)
        protected BaseController(Entities.AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ==========================================
        // Obtiene IDCLIENTE desde JWT
        // ==========================================

        protected int ObtenerIdCliente()
        {
            var claim = User.FindFirst("IDCLIENTE");

            if (claim == null)
                throw new UnauthorizedAccessException("No se encontró el claim IDCLIENTE.");

            return int.Parse(claim.Value);
        }

        // ==========================================
        // Generar Token JWT para un cliente
        // ==========================================
        protected string GenerarToken(Clientes cliente)
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
}