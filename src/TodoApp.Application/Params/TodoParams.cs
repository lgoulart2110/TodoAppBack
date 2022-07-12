using System.Linq.Expressions;
using TodoApp.Application._Common.Builders;
using TodoApp.Domain._Common.Params;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Params
{
    public class TodoParams : IFiltrable<Todo>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? TodoDate { get; set; }

        public Expression<Func<Todo, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Todo>();
            if (!string.IsNullOrWhiteSpace(Name)) predicate.And(p => p.Name.Contains(Name));
            if (!string.IsNullOrWhiteSpace(Email)) predicate.And(p => p.Email.Contains(Email));
            if (TodoDate.HasValue) predicate.And(p => p.TodoDate.Date == TodoDate.Value.Date);
            return predicate;
        }
    }
}
