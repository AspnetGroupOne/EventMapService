using Application.External.Interfaces;
using Application.External.Models;
using Application.External.Response;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Application.External.Services;

public class EventIdValidationService : IEventIdValidationService
{
    // Entire method made with help from chatgpt to bring in all the events, as there is no controller to
    // only get one event, to validate if the eventid sent is located among the events.

    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public EventIdValidationService(HttpClient httpClient, IOptions<EventSettings> options)
    {
        _httpClient = httpClient;
        _apiUrl = options.Value.Url;
    }

    public async Task<ExternalResponse> EventExistance(string eventId)
    {
        var response = await _httpClient.GetAsync(_apiUrl);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // Uses propertynamecaseinsensitive to ignore the case of the incoming data and just make them into Eventpocos.
        var events = JsonSerializer.Deserialize<List<EventPoco>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Checking if null and checking if the id exists. Needs to turn the id to a string since I treat them as strings
        // and other person treats them as ints.
        if (events == null) { return ExternalResponse.Error("Events returned are null."); }
        if (!events.Any(e => e.Id.ToString() == eventId)) { return ExternalResponse.NotFound("Event id not found among events."); }

        return ExternalResponse.Ok();
    }
}
