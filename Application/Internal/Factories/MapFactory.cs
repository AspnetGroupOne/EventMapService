using Application.Domain.Entities;
using Application.Domain.Models;

namespace Application.Internal.Factories;

public class MapFactory
{
    public static MapEntity Create(AddEventMapForm addForm)
    {
        if (addForm == null) { return null!; }
        return new MapEntity()
        {
            EventId = addForm.EventId,
            ImageUrl = addForm.ImageUrl,
            MapNodesJson = addForm.MapNodesJson
        };
    }

    public static MapEntity Create(UpdateEventMapForm updateForm)
    {
        if (updateForm == null) { return null!; }
        return new MapEntity()
        {
            EventId = updateForm.EventId,
            MapId = updateForm.MapId,
            ImageUrl = updateForm.ImageUrl,
            MapNodesJson = updateForm.MapNodesJson
        };
    }

    public static EventMap Create(MapEntity mapEntity) 
    {
        if (mapEntity == null) { return null!; }
        return new EventMap() 
        {
            EventId = mapEntity.EventId,
            MapId = mapEntity.MapId,
            ImageUrl = mapEntity.ImageUrl,
            MapNodesJson = mapEntity.MapNodesJson
        };
    }




}
