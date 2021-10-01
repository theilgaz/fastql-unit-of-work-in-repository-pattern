using System;
using System.Data;

namespace FastqlSample.Infrastructure.Core.UoW
{
    public abstract class UnitOfWorkBase
    {
        protected IDbConnection _connection;

        protected IDbTransaction _transaction;

        private bool _disposed;

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        protected abstract void ResetRepositories();

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWorkBase()
        {
            dispose(false);
        }
    }
}