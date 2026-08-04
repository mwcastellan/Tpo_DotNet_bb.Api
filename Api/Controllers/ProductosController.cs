using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tpo_DotNet_bb.Api.Api.Entities;

namespace Tpo_DotNet_bb.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly Entities.AppDbContext _context;

    public ProductosController(Entities.AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Productos>>> Get()
    {
        var productos = await _context.Productos
            .OrderBy(p => p.ID)
            .ToListAsync();

        return Ok(productos);
    }

    [HttpGet("{ID:int}")]
    public async Task<ActionResult<Productos>> Get(int ID)
    {
        var producto = await _context.Productos
            .FirstOrDefaultAsync(p => p.ID == ID);

        if (producto == null)
            return NotFound();

        return Ok(producto);
    }
}
