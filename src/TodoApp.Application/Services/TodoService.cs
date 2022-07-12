using FluentValidation;
using MapsterMapper;
using TodoApp.Application._Common.Interfaces;
using TodoApp.Application._Common.Services;
using TodoApp.Application.Models;
using TodoApp.Application.Params;
using TodoApp.Domain._Common.Adapters;
using TodoApp.Domain.Adapters;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public class TodoService : ServiceBase<Todo, TodoModel, TodoParams>, ITodoService
    {
        public TodoService(
            IMapper mapper, 
            IUnitOfWork unitOfWork, 
            IValidator<TodoModel> validator, 
            ITodoRepository repository, 
            INotificationService notificationService,
            IEmailService emailService
            ) : base(mapper, unitOfWork, validator, repository, notificationService, emailService)
        {
        }
    }
}
