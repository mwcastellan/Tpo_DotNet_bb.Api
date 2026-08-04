using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tpo_DotNet_bb.Api.Api.Entities;

namespace Tpo_DotNet_bb.Api.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Logs_ProcesosController : ControllerBase
{
    private readonly AppDbContext _context;

    public Logs_ProcesosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Logs_Procesos
                    .OrderByDescending(x => x.ID)
                    .ToListAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var log = await _context.Logs_Procesos.FindAsync(id);

        if (log == null)
            return NotFound();

        return Ok(log);
    }
}