using TodoApp.Application.Models.Abstract;
using TodoApp.Domain._Common.Params;
using TodoApp.Domain.Entities.Abstract;

namespace TodoApp.Application._Common.Interfaces
{
    public interface IServiceBase<TEntity, TModel, TParams>
        where TEntity : EntityBase
        where TModel : ModelBase
        where TParams : IParams
    {
        Task<TModel> CreateAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);
        Task<int> DeleteAsync(int id);
        Task<int> CountAsync(TParams @params);
        Task<TModel> GetByIdAsync(int id);
        Task<IEnumerable<TModel>> SearchAsync(TParams @params);
    }
}
