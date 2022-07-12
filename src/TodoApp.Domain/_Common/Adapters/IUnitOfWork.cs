namespace TodoApp.Domain._Common.Adapters
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task RollbackAsync();
        Task CommitAsync();
    }
}
