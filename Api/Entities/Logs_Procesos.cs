using System.ComponentModel.DataAnnotations.Schema;
namespace Tpo_DotNet_bb.Api.Api.Entities;

[Table("logs_procesos")]
public partial class Logs_Procesos
{
    public int ID { get; set; }

    public string MENSAJE { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
