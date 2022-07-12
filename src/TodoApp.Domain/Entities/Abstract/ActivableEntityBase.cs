namespace TodoApp.Domain.Entities.Abstract
{
    public abstract class ActivableEntityBase : EntityBase
    {
        public bool Active { get; set; }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;
    }
}
