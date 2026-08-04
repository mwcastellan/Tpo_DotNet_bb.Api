using System.ComponentModel.DataAnnotations;

namespace Tpo_DotNet_bb.Api.Api.DTOs;

public class PedidoDto
{
    [Required]
    public DateTime FECHA_COMPRA { get; set; }

    [Required]
    public int IDPRODUCTO { get; set; }

    [Required]
    public int IDESTADO { get; set; }

    [Range(0, double.MaxValue)]
    public int CANTIDAD { get; set; }

    [Range(0, double.MaxValue)]
    public double PRECIO { get; set; }

    [Range(0, double.MaxValue)]
    public double IMPORTE { get; set; }
}