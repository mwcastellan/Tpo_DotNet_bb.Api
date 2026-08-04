using System.ComponentModel.DataAnnotations.Schema;

namespace Tpo_DotNet_bb.Api.Api.Entities;

[Table("estado_pedidos")]
public class Estado_Pedidos
{
    public int ID { get; set; }

    public string DESCRIPCION { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}