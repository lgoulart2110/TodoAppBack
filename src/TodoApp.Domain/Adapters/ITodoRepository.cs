using TodoApp.Domain._Common.Adapters;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Adapters
{
    public interface ITodoRepository : IRepository<Todo>
    {
    }
}
