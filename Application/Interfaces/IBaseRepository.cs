using Application.Domain.Response;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<RepoResponse> CreateAsync(TEntity entity);
        Task<RepoResponse> DeleteAsync(TEntity entity);
        Task<RepoResponse> ExistsAsync(Expression<Func<TEntity, bool>> expression);
        Task<RepoResponse<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<RepoResponse> UpdateAsync(TEntity entity);
    }
}