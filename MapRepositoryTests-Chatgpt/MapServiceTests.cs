using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Domain.Models;
using Application.Domain.Response;
using Application.External.Interfaces;
using Application.External.Response;
using Application.Interfaces;
using Application.Internal.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Xunit;

namespace MapRepositoryTests_ChatGPT;

public class MapServiceTests
{
    private readonly Mock<IMapRepository> _repoMock;
    private readonly MapService _service;

    public MapServiceTests()
    {
        _repoMock = new Mock<IMapRepository>();
        _service = new MapService(_repoMock.Object);
    }

    [Fact]
    public async Task CreateMapAsync_NullForm_ReturnsError()
    {
        var result = await _service.CreateMapAsync(null);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("AddEventMap-form is null.");
    }

    [Fact]
    public async Task CreateMapAsync_AlreadyExists_ReturnsAlreadyExists()
    {
        _repoMock
            .Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<MapEntity, bool>>>()))
            .ReturnsAsync(RepoResponse.Ok());

        var form = new AddEventMapForm { EventId = "E1", ImageUrl = "url", Nodes = new List<MapNodes>() };
        var result = await _service.CreateMapAsync(form);

        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(409);
        result.Message.Should().Be("A map for this already exists.");
    }

    [Fact]
    public async Task CreateMapAsync_RepositoryCreateFails_ReturnsBadRequest()
    {
        _repoMock
            .Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<MapEntity, bool>>>()))
            .ReturnsAsync(RepoResponse.NotFound("not found"));
        _repoMock
            .Setup(r => r.CreateAsync(It.IsAny<MapEntity>()))
            .ReturnsAsync(RepoResponse.BadRequest("create failed"));

        var form = new AddEventMapForm { EventId = "E2", ImageUrl = "url", Nodes = new List<MapNodes>() };
        var result = await _service.CreateMapAsync(form);

        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(400);
        result.Message.Should().Be("create failed");
    }

    [Fact]
    public async Task CreateMapAsync_Success_ReturnsOk()
    {
        _repoMock
            .Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<MapEntity, bool>>>()))
            .ReturnsAsync(RepoResponse.NotFound("nf"));
        _repoMock
            .Setup(r => r.CreateAsync(It.IsAny<MapEntity>()))
            .ReturnsAsync(RepoResponse.Ok());

        var form = new AddEventMapForm { EventId = "E3", ImageUrl = "url", Nodes = new List<MapNodes>() };
        var result = await _service.CreateMapAsync(form);

        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetMapAsync_NullId_ReturnsError()
    {
        var result = await _service.GetMapAsync(null);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("EventId is null.");
    }

    [Fact]
    public async Task GetMapAsync_NotFound_ReturnsError()
    {
        _repoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<MapEntity, bool>>>()))
            .ReturnsAsync(RepoResponse<MapEntity>.Error("nf", null));

        var result = await _service.GetMapAsync("X");
        result.Success.Should().BeFalse();
        result.Message.Should().Be("The entity returned is null.");
    }

    [Fact]
    public async Task GetMapAsync_Success_ReturnsContent()
    {
        var entity = new MapEntity { EventId = "X", ImageUrl = "u", MapNodesJson = "[]" };
        _repoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<MapEntity, bool>>>()))
            .ReturnsAsync(RepoResponse<MapEntity>.Ok(entity));

        var result = await _service.GetMapAsync("X");
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType<EventMap>();
        result.Content.EventId.Should().Be("X");
    }

    [Fact]
    public async Task UpdateMapAsync_NullForm_ReturnsError()
    {
        var result = await _service.UpdateMapAsync(null);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("UpdateMap-form is null.");
    }

    [Fact]
    public async Task UpdateMapAsync_RepositoryFails_ReturnsBadRequest()
    {
        _repoMock
            .Setup(r => r.UpdateAsync(It.IsAny<MapEntity>()))
            .ReturnsAsync(RepoResponse.BadRequest("upd fail"));

        var form = new UpdateEventMapForm { EventId = "U1", ImageUrl = "url", Nodes = new List<MapNodes>() };
        var result = await _service.UpdateMapAsync(form);

        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(400);
        result.Message.Should().Be("upd fail");
    }

    [Fact]
    public async Task UpdateMapAsync_Success_ReturnsOk()
    {
        _repoMock
            .Setup(r => r.UpdateAsync(It.IsAny<MapEntity>()))
            .ReturnsAsync(RepoResponse.Ok());

        var form = new UpdateEventMapForm { EventId = "U2", ImageUrl = "url", Nodes = new List<MapNodes>() };
        var result = await _service.UpdateMapAsync(form);

        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteMapAsync_NullId_ReturnsError()
    {
        var result = await _service.DeleteMapAsync(null);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("EventId is null.");
    }

    [Fact]
    public async Task DeleteMapAsync_EntityNotFound_ReturnsError()
    {
        _repoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<MapEntity, bool>>>()))
            .ReturnsAsync(RepoResponse<MapEntity>.Error("nf", null));

        var result = await _service.DeleteMapAsync("D1");
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("The entity returned is null.");
    }

    [Fact]
    public async Task DeleteMapAsync_DeleteFails_ReturnsError()
    {
        var entity = new MapEntity { EventId = "D2", ImageUrl = "", MapNodesJson = "[]" };
        _repoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<MapEntity, bool>>>()))
            .ReturnsAsync(RepoResponse<MapEntity>.Ok(entity));
        _repoMock
            .Setup(r => r.DeleteAsync(entity))
            .ReturnsAsync(RepoResponse.Error("del fail"));

        var result = await _service.DeleteMapAsync("D2");
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("del fail");
    }

    [Fact]
    public async Task DeleteMapAsync_Success_ReturnsOk()
    {
        var entity = new MapEntity { EventId = "D3", ImageUrl = "", MapNodesJson = "[]" };
        _repoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<MapEntity, bool>>>()))
            .ReturnsAsync(RepoResponse<MapEntity>.Ok(entity));
        _repoMock
            .Setup(r => r.DeleteAsync(entity))
            .ReturnsAsync(RepoResponse.Ok());

        var result = await _service.DeleteMapAsync("D3");
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(200);
    }
}

public class MapsControllerTests
{
    private readonly Mock<IMapService> _mapServiceMock;
    private readonly Mock<IEventIdValidationService> _validationMock;
    private readonly MapsController _controller;

    public MapsControllerTests()
    {
        _mapServiceMock = new Mock<IMapService>();
        _validationMock = new Mock<IEventIdValidationService>();
        _controller = new MapsController(_mapServiceMock.Object, _validationMock.Object);
    }

    [Fact]
    public async Task CreateMap_InvalidModel_ReturnsBadRequest()
    {
        _controller.ModelState.AddModelError("x", "error");
        var result = await _controller.CreateMap(new AddEventMapForm());
        var bad = Assert.IsType<BadRequestObjectResult>(result);
        bad.Value.Should().Be("AddEventMapForm is invalid.");
    }

    [Fact]
    public async Task CreateMap_EventNotExists_ReturnsBadRequest()
    {
        _validationMock
            .Setup(v => v.EventExistance("E"))
            .ReturnsAsync(ExternalResponse.NotFound("nf"));

        var form = new AddEventMapForm { EventId = "E" };
        var result = await _controller.CreateMap(form);
        var bad = Assert.IsType<BadRequestObjectResult>(result);
        bad.Value.Should().Be("EventId does not exist.");
    }

    [Fact]
    public async Task CreateMap_ServiceFails_ReturnsBadRequest()
    {
        _validationMock
            .Setup(v => v.EventExistance("E"))
            .ReturnsAsync(ExternalResponse.Ok());
        _mapServiceMock
            .Setup(s => s.CreateMapAsync(It.IsAny<AddEventMapForm>()))
            .ReturnsAsync(ServResponse.Error("oops"));

        var result = await _controller.CreateMap(new AddEventMapForm { EventId = "E" });
        var bad = Assert.IsType<BadRequestObjectResult>(result);
        bad.Value.Should().Be("oops");
    }

    [Fact]
    public async Task CreateMap_Success_ReturnsOk()
    {
        _validationMock
            .Setup(v => v.EventExistance("E"))
            .ReturnsAsync(ExternalResponse.Ok());
        var resp = ServResponse.Ok();
        _mapServiceMock
            .Setup(s => s.CreateMapAsync(It.IsAny<AddEventMapForm>()))
            .ReturnsAsync(resp);

        var result = await _controller.CreateMap(new AddEventMapForm { EventId = "E" });
        var ok = Assert.IsType<OkObjectResult>(result);
        ok.Value.Should().Be(resp);
    }

    [Fact]
    public async Task GetMap_ServiceFails_ReturnsBadRequest()
    {
        _mapServiceMock
            .Setup(s => s.GetMapAsync("X"))
            .ReturnsAsync(ServResponse<EventMap>.Error("no map", null));

        var bad = await _controller.GetMap("X") as BadRequestObjectResult;
        bad.Value.Should().Be("no map");
    }

    [Fact]
    public async Task GetMap_Success_ReturnsOk()
    {
        var map = new EventMap { EventId = "X" };
        _mapServiceMock
            .Setup(s => s.GetMapAsync("X"))
            .ReturnsAsync(ServResponse<EventMap>.Ok(map));

        var ok = await _controller.GetMap("X") as OkObjectResult;
        var body = Assert.IsType<ServResponse<EventMap>>(ok.Value);
        body.Content.EventId.Should().Be("X");
    }

    [Fact]
    public async Task UpdateMap_InvalidModel_ReturnsBadRequest()
    {
        _controller.ModelState.AddModelError("x", "error");
        var bad = await _controller.UpdateMap(new UpdateEventMapForm()) as BadRequestObjectResult;
        bad.Value.Should().Be("UpdateEventMapForm is invalid.");
    }

    [Fact]
    public async Task UpdateMap_ServiceFails_ReturnsBadRequest()
    {
        _mapServiceMock
            .Setup(s => s.UpdateMapAsync(It.IsAny<UpdateEventMapForm>()))
            .ReturnsAsync(ServResponse.Error("u-fail"));

        var bad = await _controller.UpdateMap(new UpdateEventMapForm { EventId = "U" }) as BadRequestObjectResult;
        bad.Value.Should().Be("u-fail");
    }

    [Fact]
    public async Task UpdateMap_Success_ReturnsOk()
    {
        _mapServiceMock
            .Setup(s => s.UpdateMapAsync(It.IsAny<UpdateEventMapForm>()))
            .ReturnsAsync(ServResponse.Ok());

        var ok = await _controller.UpdateMap(new UpdateEventMapForm { EventId = "U" }) as OkObjectResult;
        ok.Value.Should().BeOfType<ServResponse>();
    }

    [Fact]
    public async Task DeleteMap_BadInput_ReturnsBadRequest()
    {
        var bad = await _controller.DeleteMap(null) as BadRequestObjectResult;
        bad.Value.Should().Be("EventId given is null.");
    }

    [Fact]
    public async Task DeleteMap_ServiceFails_ReturnsBadRequest()
    {
        _mapServiceMock
            .Setup(s => s.DeleteMapAsync("D"))
            .ReturnsAsync(ServResponse.Error("d-fail"));

        var bad = await _controller.DeleteMap("D") as BadRequestObjectResult;
        bad.Value.Should().Be("d-fail");
    }

    [Fact]
    public async Task DeleteMap_Success_ReturnsOk()
    {
        _mapServiceMock
            .Setup(s => s.DeleteMapAsync("D"))
            .ReturnsAsync(ServResponse.Ok());

        var ok = await _controller.DeleteMap("D") as OkObjectResult;
        ok.Value.Should().BeOfType<ServResponse>();
    }
}
