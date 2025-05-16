using Application.Data.Context;
using Application.Domain.Entities;

namespace Application.Data.Repository;

public class MapRepository(DataContext context) : BaseRepository<MapEntity>(context) 
{
}
