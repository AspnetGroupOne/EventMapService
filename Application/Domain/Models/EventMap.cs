namespace Application.Domain.Models;

public class EventMap
{
    public string MapId { get; set; } = null!;
    public string EventId { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string? MapNodesJson { get; set; }
}
