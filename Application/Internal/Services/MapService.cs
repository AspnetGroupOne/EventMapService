using Application.Domain.Models;
using Application.Domain.Response;
using Application.Interfaces;
using Application.Internal.Factories;

namespace Application.Internal.Services;

public class MapService(IMapRepository mapRepository) : IMapService
{
    private readonly IMapRepository _mapRepository = mapRepository;


    public async Task<ServResponse> CreateMapAsync(AddEventMapForm addEventMapForm)
    {
        try
        {
            if (addEventMapForm == null) { return ServResponse.Error("AddEventMap-form is null."); }

            var exists = await _mapRepository.ExistsAsync(entity => entity.EventId == addEventMapForm.EventId);
            if (exists.Success) { return ServResponse.AlreadyExists("A map for this already exists."); }

            var mapEntity = MapFactory.Create(addEventMapForm);
            if (mapEntity == null) { return ServResponse.Error("MapEntity returned from the factory is null."); }

            var result = await _mapRepository.CreateAsync(mapEntity);
            if (!result.Success) { return ServResponse.BadRequest(result.Message); }

            return ServResponse.Ok();
        }
        catch (Exception ex)
        {
            return ServResponse.Error(ex.Message);
        }
    }
    public async Task<ServResponse<EventMap>> GetMapAsync(string eventId)
    {
        try
        {
            if (eventId == null) { return ServResponse<EventMap>.Error("EventId is null.", null); }

            var mapResult = await _mapRepository.GetAsync(e => e.EventId == eventId);
            if (mapResult.Content == null) { return ServResponse<EventMap>.Error("The entity returned is null.", null); }

            var map = MapFactory.Create(mapResult.Content);
            if (map == null) { return ServResponse<EventMap>.Error("Map returned from factory is null.", null); }

            return ServResponse<EventMap>.Ok(map);
        }
        catch (Exception ex)
        {
            return ServResponse<EventMap>.Error(ex.Message, null);
        }
    }

    public async Task<ServResponse> UpdateMapAsync(UpdateEventMapForm updateEventMapForm)
    {
        try
        {
            if (updateEventMapForm == null) { return ServResponse.Error("UpdateMap-form is null."); }

            var updateEntity = MapFactory.Create(updateEventMapForm);
            if (updateEntity == null) { return ServResponse.Error("UpdateMapentity returned from the factory is null."); }

            var result = await _mapRepository.UpdateAsync(updateEntity);
            if (!result.Success) { return ServResponse.BadRequest(result.Message); }

            return ServResponse.Ok();
        }
        catch (Exception ex)
        {
            return ServResponse.Error(ex.Message);
        }
    }

    public async Task<ServResponse> DeleteMapAsync(string eventId)
    {
        try
        {
            if (eventId == null) { return ServResponse.Error("EventId is null."); }

            var mapEntityDelete = await _mapRepository.GetAsync(e => e.EventId == eventId);
            if (mapEntityDelete.Content == null) { return ServResponse.Error("The entity returned is null."); }

            var result = await _mapRepository.DeleteAsync(mapEntityDelete.Content);
            if (!result.Success) { return ServResponse.Error($"Something went wrong when trying to remove entity. {result.Message}"); }

            return ServResponse.Ok();
        }
        catch (Exception ex)
        {
            return ServResponse.Error(ex.Message);
        }
    }
}
