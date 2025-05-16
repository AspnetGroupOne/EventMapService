namespace Application.Domain.Models;

public class AddEventMapForm
{
    public string ImageUrl { get; set; } = null!;
    public string? MapNodesJson { get; set; }
}
