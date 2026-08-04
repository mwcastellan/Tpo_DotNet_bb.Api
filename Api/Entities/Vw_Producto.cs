using System.ComponentModel.DataAnnotations.Schema;
namespace Tpo_DotNet_bb.Api.Api.Entities;

[Table("vw_producto")]
public partial class Vw_Producto
{
    public int ID { get; set; }

    public string DESCRIPCION { get; set; } = null!;

    public string DESCRIPCION_AMPLIA { get; set; } = null!;

    public DateTime FECHA_VTO { get; set; }

    public int STOCK_DISPONIBLE { get; set; }

    public string NOMBRE_IMAGEN { get; set; } = null!;

    public decimal PRECIO { get; set; }

    public int IDSUBCATEGORIA { get; set; }

    public string DESCRIPCION_SUBCATEGORIA { get; set; } = null!;

    public string PATH_SUBCATEGORIA { get; set; } = null!;

    public int IDCATEGORIA { get; set; }

    public string DESCRIPCION_CATEGORIA { get; set; } = null!;

    public string PATH_CATEGORIA { get; set; } = null!;
}