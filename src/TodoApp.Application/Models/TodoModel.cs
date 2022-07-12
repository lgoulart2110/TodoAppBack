using TodoApp.Application.Models.Abstract;
using TodoApp.Domain.Enums;

namespace TodoApp.Application.Models
{
    public class TodoModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? TodoDate { get; set; }
        public string Email { get; set; }
        public TodoStatus? Status { get; set; }
    }
}
