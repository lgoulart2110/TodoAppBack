using TodoApp.Api.Controllers.Abstract;
using TodoApp.Application._Common.Interfaces;
using TodoApp.Application.Models;
using TodoApp.Application.Params;
using TodoApp.Domain.Entities;

namespace TodoApp.Api.Controllers
{
    public class TodoController : BaseController<Todo, TodoModel, TodoParams>
    {
        public TodoController(INotificationService notificationService, ITodoService service) : base(notificationService, service)
        {
        }
    }
}
