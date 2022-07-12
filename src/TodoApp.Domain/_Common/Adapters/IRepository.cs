using System.Linq.Expressions;
using TodoApp.Domain._Common.Params;
using TodoApp.Domain.Entities.Abstract;

namespace TodoApp.Domain._Common.Adapters
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> DeleteByIdAsync(int id);
        Task<int> CountAsync(IFiltrable<TEntity> filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> ExistAsync(int id);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter, string orderBy = null);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAllAsync(IPaginable pagination = null, string orderBy = null);
        Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> filter, IPaginable pagination = null, string orderBy = null);
        Task<IEnumerable<TEntity>> SearchAsync(IParams @params);
    }
}
