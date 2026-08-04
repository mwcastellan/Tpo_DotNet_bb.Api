using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tpo_DotNet_bb.Api.Api.Entities;

[Table("clientes")]
public partial class Clientes
{
    public int ID { get; set; }
    [EmailAddress]
    public string EMAIL { get; set; } = null!;

    public string APELLIDO { get; set; } = null!;

    public string NOMBRE { get; set; } = null!;

    public string DIRECCION { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? PASSWORD { get; set; }

}
