namespace Application.Domain.Models;

public class EventMap
{
    public string EventId { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public List<MapNodes> Nodes { get; set; } = new();
}
