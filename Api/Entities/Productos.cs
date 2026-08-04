using System.ComponentModel.DataAnnotations.Schema;
namespace Tpo_DotNet_bb.Api.Api.Entities;

[Table("productos")]
public partial class Productos
{
    public int ID { get; set; }

    public string DESCRIPCION { get; set; } = null!;

    public string DESCRIPCION_AMPLIA { get; set; } = null!;

    public DateOnly FECHA_VTO { get; set; }

    public int STOCK_DISPONIBLE { get; set; }

    public string NOMBRE_IMAGEN { get; set; } = null!;

    public decimal PRECIO { get; set; }

    public int IDSUBCATEGORIA { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
