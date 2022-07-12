using System.Linq.Expressions;
using TodoApp.Domain.Entities.Abstract;

namespace TodoApp.Domain._Common.Params
{
    public interface IFiltrable<TEntity> : IParams where TEntity : EntityBase
    {
        Expression<Func<TEntity, bool>> Filter();
    }
}
