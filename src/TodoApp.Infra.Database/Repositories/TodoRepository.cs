using TodoApp.Domain.Adapters;
using TodoApp.Domain.Entities;
using TodoApp.Infra.Database._Common.Persistence;
using TodoApp.Infra.Database.Context;

namespace TodoApp.Infra.Database.Repositories
{
    public class TodoRepository : RepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(TodoContext todoContext) : base(todoContext)
        {
        }
    }
}
