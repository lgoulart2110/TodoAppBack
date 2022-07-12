namespace TodoApp.Domain.Entities.Abstract
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
