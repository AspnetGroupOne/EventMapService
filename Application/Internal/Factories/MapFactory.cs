using Application.Domain.Entities;
using Application.Domain.Models;
using System.Text.Json;
using System.Collections.Generic;

namespace Application.Internal.Factories;

public class MapFactory
{
    public static MapEntity Create(AddEventMapForm addForm)
    {
        if (addForm == null)
            return null!;

        return new MapEntity
        {
            EventId = addForm.EventId,
            ImageUrl = addForm.ImageUrl,
            MapNodesJson = JsonSerializer.Serialize(addForm.Nodes)
        };
    }

    public static MapEntity Create(UpdateEventMapForm updateForm)
    {
        if (updateForm == null)
            return null!;

        return new MapEntity
        {
            EventId = updateForm.EventId,
            ImageUrl = updateForm.ImageUrl,
            MapNodesJson = JsonSerializer.Serialize(updateForm.Nodes)
        };
    }

    public static EventMap Create(MapEntity mapEntity)
    {
        if (mapEntity == null)
            return null!;


        // This fix made by chatgpt after issues where this would return null och mapnode names and gridids. 
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var nodes = string.IsNullOrWhiteSpace(mapEntity.MapNodesJson)
            ? new List<MapNodes>()
            : JsonSerializer.Deserialize<List<MapNodes>>(mapEntity.MapNodesJson, options)
              ?? new List<MapNodes>();

        return new EventMap
        {
            EventId = mapEntity.EventId,
            ImageUrl = mapEntity.ImageUrl,
            Nodes = nodes
        };
    }
}
