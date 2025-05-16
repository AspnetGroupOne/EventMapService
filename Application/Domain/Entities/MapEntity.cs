using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Domain.Entities;

[Table("Maps")]
public class MapEntity
{
    [Key]
    public string MapId { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string? MapNodesJson { get; set; }
}
