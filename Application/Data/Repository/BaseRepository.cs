using Application.Data.Context;
using Application.Domain.Response;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Data.Repository;

public class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();


    public virtual async Task<RepoResponse> CreateAsync(TEntity entity)
    {
        try
        {
            if (entity == null) { return RepoResponse.Error("Entity is null."); }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return RepoResponse.Ok();
        }
        catch (Exception ex) { return RepoResponse.Error(ex.Message); }
    }

    public virtual async Task<RepoResponse> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            if (expression == null) { return RepoResponse.Error("Expression is invalid."); }

            var exists = await _dbSet.AnyAsync(expression);
            if (!exists) { return RepoResponse.NotFound("Entity not found."); }

            return RepoResponse.Ok();
        }
        catch (Exception ex) { return RepoResponse.Error(ex.Message); }
    }
    public virtual async Task<RepoResponse<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            if (expression == null) { return RepoResponse<TEntity>.Error("Expression is invalid.", null); }

            var entity = await _dbSet.FirstOrDefaultAsync(expression);
            if (entity == null) { return RepoResponse<TEntity>.Error("Something went wrong when getting entity. Entity is null.", null); }

            return RepoResponse<TEntity>.Ok(entity);
        }
        catch (Exception ex) { return RepoResponse<TEntity>.Error(ex.Message, null); }
    }

    public virtual async Task<RepoResponse> UpdateAsync(TEntity entity)
    {
        try
        {
            if (entity == null) { return RepoResponse.Error("Entity is null."); }

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return RepoResponse.Ok();
        }
        catch (Exception ex) { return RepoResponse.Error(ex.Message); }
    }

    public virtual async Task<RepoResponse> DeleteAsync(TEntity entity)
    {
        try
        {
            if (entity == null) { return RepoResponse.Error("Entity is null."); }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return RepoResponse.Ok();
        }
        catch (Exception ex) { return RepoResponse.Error(ex.Message); }
    }
}
