using Application.Data.Context;
using Application.Domain.Entities;
using Application.Interfaces;

namespace Application.Data.Repository;

public class MapRepository(DataContext context) : BaseRepository<MapEntity>(context), IMapRepository
{
}
