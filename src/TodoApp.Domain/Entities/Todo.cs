using TodoApp.Domain.Entities.Abstract;
using TodoApp.Domain.Enums;

namespace TodoApp.Domain.Entities
{
    public class Todo : ActivableEntityBase
    {
        public string Description { get; set; }
        public DateTime TodoDate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public TodoStatus Status { get; set; }
    }
}
