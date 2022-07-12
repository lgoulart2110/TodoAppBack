using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Models;

namespace TodoApp.Application.Validators
{
    public class TodoValidator : AbstractValidator<TodoModel>
    {
        public TodoValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(6)
                .MaximumLength(150)
                .NotEmpty();

            RuleFor(x => x.Description)
                .MinimumLength(6)
                .MaximumLength(500)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Status)
                .NotNull();

            RuleFor(x => x.TodoDate)
                .NotNull();
        }
    }
}
