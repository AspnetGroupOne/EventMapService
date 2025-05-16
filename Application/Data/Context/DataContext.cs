using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    
    public virtual DbSet<MapEntity> Maps { get; set; }




}
