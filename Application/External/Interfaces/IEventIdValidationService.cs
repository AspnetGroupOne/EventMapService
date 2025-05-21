using Application.External.Response;

namespace Application.External.Interfaces
{
    public interface IEventIdValidationService
    {
        Task<ExternalResponse> EventExistance(string eventId);
    }
}