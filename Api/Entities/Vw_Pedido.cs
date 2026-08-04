using System.ComponentModel.DataAnnotations.Schema;
namespace Tpo_DotNet_bb.Api.Api.Entities;

[Table("vw_pedido")]
public partial class Vw_Pedido
{
    public int ID { get; set; }

    public DateOnly FECHA_COMPRA { get; set; }

    public int IDCLIENTE { get; set; }

    public int IDPRODUCTO { get; set; }

    public int CANTIDAD { get; set; }

    public double PRECIO { get; set; }

    public double IMPORTE { get; set; }

    public int IDESTADO { get; set; }

    public string EMAIL_CLIENTE { get; set; } = null!;

    public string DESCRIPCION_PRODUCTO { get; set; } = null!;

    public string DESCRIPCION_SUBCATEGORIA { get; set; } = null!;

    public string DESCRIPCION_CATEGORIA { get; set; } = null!;

    public string DESCRIPCION_ESTADO_PEDIDOS { get; set; } = null!;
}
