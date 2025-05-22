using Application.Domain.Models;
using Application.Domain.Response;

namespace Application.Interfaces
{
    public interface IMapService
    {
        Task<ServResponse> CreateMapAsync(AddEventMapForm addEventMapForm);
        Task<ServResponse> DeleteMapAsync(string eventId);
        Task<ServResponse<EventMap>> GetMapAsync(string eventId);
        Task<ServResponse> UpdateMapAsync(UpdateEventMapForm updateEventMapForm);
    }
}