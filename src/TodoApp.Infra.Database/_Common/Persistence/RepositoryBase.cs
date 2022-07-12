using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoApp.Domain._Common.Adapters;
using TodoApp.Domain._Common.Params;
using TodoApp.Domain.Entities.Abstract;
using TodoApp.Infra.Database.Context;
using TodoApp.Infra.Database._Common.Extensions;
using TodoApp.Domain.Models;

namespace TodoApp.Infra.Database._Common.Persistence
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(TodoContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<int> CountAsync(IFiltrable<TEntity> filter = null)
        {
            return await CountAsync(filter?.Filter());
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await GetQueryable(filter).CountAsync();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return await Task.FromResult(_dbSet.Remove(entity).Entity);
        }

        public virtual async Task<TEntity> DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null ? await DeleteAsync(entity) : default;
        }

        public virtual async Task<bool> ExistAsync(int id)
        {
            return await ExistAsync(entity => entity.Id.Equals(id));
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            return await GetQueryable(filter).AnyAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(IPaginable pagination = null, string orderBy = null)
        {
            return await GetQueryable(pagination: pagination ?? new Pagination(), orderBy: orderBy).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await GetQueryable(entity => entity.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter, string orderBy = null)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            return await GetQueryable(filter, orderBy: orderBy).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            return await GetQueryable(filter).SingleOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> filter, IPaginable pagination = null, string orderBy = null)
        {
            return await GetQueryable(filter, pagination ?? new Pagination(), orderBy).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> SearchAsync(IParams @params)
        {
            var filter = @params as IFiltrable<TEntity>;
            var pagination = @params as IPaginable;
            var sorting = @params as ISortable;
            return await SearchAsync(filter.Filter(), pagination, sorting?.OrderBy);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entityEntry = _dbSet.Update(entity);
            return await Task.FromResult(entityEntry.Entity);
        }

        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            IPaginable pagination = null,
            string orderBy = null
        )
        {
            var query = _dbSet.AsQueryable();

            if (filter != null) query = query.Where(filter);

            if (pagination != null)
            {
                query = query.Skip(pagination.Skip.GetValueOrDefault());
                if (pagination.Take.HasValue) query = query.Take(pagination.Take.Value);
            }

            if (!string.IsNullOrWhiteSpace(orderBy)) query.OrderBy(orderBy);

            return query;
        }
    }
}
