using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tpo_DotNet_bb.Api.Entities;

namespace Tpo_DotNet_bb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Reporte01Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public Reporte01Controller(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vw_Productos>>> Get()
        {
            var productos = await _context.Vw_Productos
                .OrderBy(x => x.IDCATEGORIA)
                .ThenBy(x => x.IDSUBCATEGORIA)
                .ThenBy(x => x.ID)
                .ToListAsync();

            return Ok(productos);
        }
    }
}
