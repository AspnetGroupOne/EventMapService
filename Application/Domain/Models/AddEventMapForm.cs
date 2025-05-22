namespace Application.Domain.Models;

public class AddEventMapForm
{
    public string EventId { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public List<MapNodes> Nodes { get; set; } = new();
}
