using System.Linq.Expressions;
using TodoApp.Application._Common.Builders;
using TodoApp.Domain._Common.Params;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Params
{
    public class TodoParams : IFiltrable<Todo>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime? TodoDate { get; set; }

        public Expression<Func<Todo, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Todo>();
            if (!string.IsNullOrWhiteSpace(Name)) predicate = predicate.And(p => p.Name.Contains(Name));
            if (!string.IsNullOrWhiteSpace(Email)) predicate = predicate.And(p => p.Email.Contains(Email));
            if (!string.IsNullOrWhiteSpace(Description)) predicate = predicate.And(p => p.Description.Contains(Description));
            if (TodoDate.HasValue) predicate = predicate.And(p => p.TodoDate.Date == TodoDate.Value.Date);
            return predicate;
        }
    }
}
