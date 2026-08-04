using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tpo_DotNet_bb.Api.Api.Entities;

namespace Tpo_DotNet_bb.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Estado_PedidosController : ControllerBase
{
    private readonly AppDbContext _context;

    public Estado_PedidosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Estado_Pedidos.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var estado = await _context.Estado_Pedidos.FindAsync(id);

        if (estado == null)
            return NotFound();

        return Ok(estado);
    }
}
