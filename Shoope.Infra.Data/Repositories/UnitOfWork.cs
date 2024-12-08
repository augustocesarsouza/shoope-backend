using Microsoft.EntityFrameworkCore.Storage;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            //if (_transaction != null)
            //{
            //    await _transaction.CommitAsync();
            //}

            try
            {
                if (_transaction != null)
                {
                    await _context.SaveChangesAsync();
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public async Task Rollback()
        {
            //if (_transaction != null)
            //{
            //    await _transaction.RollbackAsync();
            //}

            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            //_transaction?.Dispose();

            _transaction?.Dispose();
            _transaction = null;
        }
    }
}
