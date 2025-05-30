using Application.Data.Context;
using Application.Data.Repository;
using Application.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace MapRepositoryTests_Chatgpt;

public class MapRepositoryTests : IDisposable
{
    private readonly DataContext _context;
    private readonly MapRepository _repo;

    public MapRepositoryTests()
    {
        // each test gets its own in-memory database
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new DataContext(options);
        _repo = new MapRepository(_context);
    }

    [Fact]
    public async Task CreateAsync_NullEntity_ReturnsError()
    {
        var result = await _repo.CreateAsync(null);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Entity is null.");
    }

    [Fact]
    public async Task CreateAsync_ValidEntity_AddsToDatabase()
    {
        var entity = new MapEntity { EventId = "E1", ImageUrl = "u1", MapNodesJson = "[]" };

        var result = await _repo.CreateAsync(entity);
        result.Success.Should().BeTrue();

        var saved = await _context.Maps.FindAsync("E1");
        saved.Should().NotBeNull();
        saved.ImageUrl.Should().Be("u1");
    }

    [Fact]
    public async Task ExistsAsync_NullExpression_ReturnsError()
    {
        var result = await _repo.ExistsAsync(null!);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Expression is invalid.");
    }

    [Fact]
    public async Task ExistsAsync_NotFound_ReturnsNotFound()
    {
        var result = await _repo.ExistsAsync(e => e.EventId == "nope");
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task ExistsAsync_Found_ReturnsOk()
    {
        _context.Maps.Add(new MapEntity { EventId = "E2", ImageUrl = "u2", MapNodesJson = "[]" });
        await _context.SaveChangesAsync();

        var result = await _repo.ExistsAsync(e => e.EventId == "E2");
        result.Success.Should().BeTrue();
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAsync_NullExpression_ReturnsError()
    {
        var result = await _repo.GetAsync(null!);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Expression is invalid.");
    }

    [Fact]
    public async Task GetAsync_NotFound_ReturnsError()
    {
        var result = await _repo.GetAsync(e => e.EventId == "none");
        result.Success.Should().BeFalse();
        result.StatusCode.Should().Be(500);
        result.Message.Should().Contain("Entity is null");
    }

    [Fact]
    public async Task GetAsync_Found_ReturnsEntity()
    {
        var entity = new MapEntity { EventId = "E3", ImageUrl = "u3", MapNodesJson = "[]" };
        _context.Maps.Add(entity);
        await _context.SaveChangesAsync();

        var result = await _repo.GetAsync(e => e.EventId == "E3");
        result.Success.Should().BeTrue();
        result.Content.Should().NotBeNull();
        result.Content.EventId.Should().Be("E3");
    }

    [Fact]
    public async Task UpdateAsync_NullEntity_ReturnsError()
    {
        var result = await _repo.UpdateAsync(null);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Entity is null.");
    }

    [Fact]
    public async Task UpdateAsync_ValidEntity_UpdatesDatabase()
    {
        var entity = new MapEntity { EventId = "E4", ImageUrl = "u4", MapNodesJson = "[]" };
        _context.Maps.Add(entity);
        await _context.SaveChangesAsync();

        entity.ImageUrl = "u4-updated";
        var result = await _repo.UpdateAsync(entity);
        result.Success.Should().BeTrue();

        var updated = await _context.Maps.FindAsync("E4");
        updated!.ImageUrl.Should().Be("u4-updated");
    }

    [Fact]
    public async Task DeleteAsync_NullEntity_ReturnsError()
    {
        var result = await _repo.DeleteAsync(null);
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Entity is null.");
    }

    [Fact]
    public async Task DeleteAsync_ValidEntity_RemovesFromDatabase()
    {
        var entity = new MapEntity { EventId = "E5", ImageUrl = "u5", MapNodesJson = "[]" };
        _context.Maps.Add(entity);
        await _context.SaveChangesAsync();

        var result = await _repo.DeleteAsync(entity);
        result.Success.Should().BeTrue();

        var exists = await _context.Maps.FindAsync("E5");
        exists.Should().BeNull();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}