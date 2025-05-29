using Application.Domain.Models;
using Application.External.Interfaces;
using Application.External.Services;
using Application.Interfaces;
using Application.Internal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions.Attributes;

namespace Presentation.Controllers;

[UseApiKey]
[Route("api/[controller]")]
[ApiController]
public class MapsController(IMapService mapService, IEventIdValidationService eventValidation) : ControllerBase
{
    private readonly IMapService _mapService = mapService;
    private readonly IEventIdValidationService _eventValidation = eventValidation;


    [HttpPost]
    public async Task<IActionResult> CreateMap(AddEventMapForm addForm)
    {
        if (!ModelState.IsValid) { return BadRequest("AddEventMapForm is invalid."); }

        var eventExistance = await _eventValidation.EventExistance(addForm.EventId);
        if (!eventExistance.Success) { return BadRequest("EventId does not exist."); }

        var result = await _mapService.CreateMapAsync(addForm);
        if (!result.Success) { return BadRequest(result.Message); }

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMap(UpdateEventMapForm updateForm)
    {
        if (!ModelState.IsValid) { return BadRequest("UpdateEventMapForm is invalid."); }

        var result = await _mapService.UpdateMapAsync(updateForm);
        if (!result.Success) { return BadRequest(result.Message); }

        return Ok(result);
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetMap(string eventId)
    {
        if (eventId == null) { return BadRequest("EventId is null."); }

        var result = await _mapService.GetMapAsync(eventId);
        if (!result.Success) { return BadRequest(result.Message); }

        return Ok(result);
    }

    [HttpDelete("{eventId}")]
    public async Task<IActionResult> DeleteMap(string eventId) 
    {
        if (eventId == null) { return BadRequest("EventId given is null."); }

        var result = await _mapService.DeleteMapAsync(eventId);
        if (!result.Success) { return BadRequest(result.Message); }

        return Ok(result);
    }
}
