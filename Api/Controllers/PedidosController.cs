using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tpo_DotNet_bb.Api.DTOs;
using Tpo_DotNet_bb.Api.Entities;
using Tpo_DotNet_bb.Api.Services;

namespace Tpo_DotNet_bb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly Entities.AppDbContext _context;
    private readonly ILogProcesoService _logService;

    public PedidosController(
        Entities.AppDbContext context,
        ILogProcesoService logService)
    {
        _context = context;
        _logService = logService;
    }


    // ==========================================
    // GET api/pedidos
    // ==========================================
    [HttpGet]
    public async Task<IActionResult> GetPedidos()
    {
        int idCliente = ObtenerIdCliente();

        var pedidos = await _context.Pedidos
            .Where(x => x.IDCLIENTE == idCliente)
            .OrderByDescending(x => x.FECHA_COMPRA)
            .ToListAsync();

        return Ok(pedidos);
    }

    // ==========================================
    // GET api/pedidos/5
    // ==========================================
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPedido(int id)
    {
        int idCliente = ObtenerIdCliente();

        var pedido = await _context.Pedidos
            .FirstOrDefaultAsync(
                x => x.ID == id &&
                     x.IDCLIENTE == idCliente);

        if (pedido == null)
            return NotFound();

        return Ok(pedido);
    }

    // ==========================================
    // POST api/pedidos
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> Crear(
        PedidoDto dto)
    {
        int idCliente = ObtenerIdCliente();

        var pedido = new Pedidos
        {
            FECHA_COMPRA = dto.FECHA_COMPRA,
            IDCLIENTE = idCliente,
            IDPRODUCTO = dto.IDPRODUCTO,
            IDESTADO = dto.IDESTADO,
            CANTIDAD = dto.CANTIDAD,
            PRECIO = dto.PRECIO,
            IMPORTE = dto.IMPORTE,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        _context.Pedidos.Add(pedido);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje = "Pedido creado correctamente"
        });

        // await _logService.GrabarAsync($"Crear Pedido - IDCLIENTE {idCliente}");

    }

    // ==========================================
    // PUT api/pedidos/5
    // ==========================================
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        PedidoDto dto)
    {
        int idCliente = ObtenerIdCliente();

        var Pedidos = await _context.Pedidos
            .FirstOrDefaultAsync(
                x => x.ID == id &&
                     x.IDCLIENTE == idCliente);

        if (Pedidos == null)
            return NotFound();

        Pedidos.FECHA_COMPRA = dto.FECHA_COMPRA;
        Pedidos.IDPRODUCTO = dto.IDPRODUCTO;
        Pedidos.IDESTADO = dto.IDESTADO;
        Pedidos.CANTIDAD = dto.CANTIDAD;
        Pedidos.PRECIO = dto.PRECIO;
        Pedidos.IMPORTE = dto.IMPORTE;
        Pedidos.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje =
                "Pedido actualizado correctamente"
        });
    }

    // ==========================================
    // DELETE api/pedidos/5
    // ==========================================
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(
        int id)
    {
        int idCliente = ObtenerIdCliente();

        var pedido = await _context.Pedidos
            .FirstOrDefaultAsync(
                x => x.ID == id &&
                     x.IDCLIENTE == idCliente);

        if (pedido == null)
            return NotFound();

        _context.Pedidos.Remove(pedido);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje =
                "Pedido eliminado correctamente"
        });
    }

    // ==========================================
    // Obtiene IDCLIENTE desde JWT
    // ==========================================
    private int ObtenerIdCliente()
    {
        var claim = User.FindFirst("IDCLIENTE");

        if (claim == null)
            throw new UnauthorizedAccessException();

        return int.Parse(claim.Value);
    }
}