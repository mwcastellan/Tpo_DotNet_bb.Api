using System.ComponentModel.DataAnnotations.Schema;

namespace Tpo_DotNet_bb.Api.Entities;

[Table("categoria")]
public partial class Categoria
{
    public int ID { get; set; }

    public string DESCRIPCION { get; set; } = null!;

    public string PATH_IMAGEN { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
