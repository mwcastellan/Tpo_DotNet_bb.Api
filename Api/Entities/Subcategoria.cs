using System.ComponentModel.DataAnnotations.Schema;

namespace Tpo_DotNet_bb.Api.Api.Entities;

[Table("subcategoria")]
public partial class Subcategoria
{
    public int ID { get; set; }

    public string DESCRIPCION { get; set; } = null!;

    public string PATH_IMAGEN { get; set; } = null!;

    public int IDCATEGORIA { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
