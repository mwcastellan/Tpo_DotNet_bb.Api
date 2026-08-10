using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tpo_DotNet_bb.Api.Entities;

namespace Tpo_DotNet_bb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubcategoriaController : ControllerBase
{
    private readonly Entities.AppDbContext _context;

    public SubcategoriaController(Entities.AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subcategoria>>> Get()
    {
        var subcategoria = await _context.Subcategoria
            .OrderBy(s => s.ID)
            .ToListAsync();

        return Ok(subcategoria);
    }

    [HttpGet("{ID:int}")]
    public async Task<ActionResult<Subcategoria>> Get(int ID)
    {
        var subcategoria = await _context.Subcategoria
            .FirstOrDefaultAsync(s => s.ID == ID);

        if (subcategoria == null)
            return NotFound();

        return Ok(subcategoria);
    }
}