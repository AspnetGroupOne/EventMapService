namespace Application.External.Models;

public class EventPoco
{
    // Learned about POCO's from chatgpt. It is basically a model shaped the same way as the model in the other api
    // into which you load and hold the data for the moment you need it.
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? Date { get; set; }
    public string? Location { get; set; }
}
