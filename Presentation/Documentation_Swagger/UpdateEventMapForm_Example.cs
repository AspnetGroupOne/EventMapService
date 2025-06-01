using Application.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation_Swagger;

public class UpdateEventMapForm_Example : IExamplesProvider<UpdateEventMapForm>
{
    public UpdateEventMapForm GetExamples() => new()
    {
        EventId = "56f58514-7581-4b18-97f5-b6eb5ba7b9c9",
        ImageUrl = "www.exampleurl.com",
        Nodes = new List<MapNodes>
        {
            new MapNodes
            {
                NodeType = "Bathroom",
                GridId = 1
            }
        }
    };
}