using System.ComponentModel.DataAnnotations.Schema;

namespace Tpo_DotNet_bb.Api.Entities;

[Table("pedidos")]
public partial class Pedidos
{
    public int ID { get; set; }

    public DateTime FECHA_COMPRA { get; set; }

    public int IDCLIENTE { get; set; }

    public int IDPRODUCTO { get; set; }

    public int CANTIDAD { get; set; }

    public double PRECIO { get; set; }

    public double IMPORTE { get; set; }

    public int IDESTADO { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime FECHA_ENVIO { get; set; }

}
