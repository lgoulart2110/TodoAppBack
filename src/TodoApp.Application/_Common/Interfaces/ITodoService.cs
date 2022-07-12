using TodoApp.Application.Models;
using TodoApp.Application.Params;
using TodoApp.Domain.Entities;

namespace TodoApp.Application._Common.Interfaces
{
    public interface ITodoService : IServiceBase<Todo, TodoModel, TodoParams>
    {
    }
}
