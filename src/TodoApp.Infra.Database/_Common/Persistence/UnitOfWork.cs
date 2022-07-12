using Microsoft.EntityFrameworkCore.Storage;
using TodoApp.Domain._Common.Adapters;
using TodoApp.Infra.Database.Context;

namespace TodoApp.Infra.Database._Common.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TodoContext _context;
        private IDbContextTransaction _currentTransaction => _context.Database.CurrentTransaction;

        public UnitOfWork(TodoContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) return;
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_currentTransaction == null) throw new InvalidOperationException("CurrentTransaction is null please call BeginTransactionAsync() before commit.");

            try
            {
                await _context.SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync();
            }
            finally
            {
                _currentTransaction?.Dispose();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
        }
    }
}
