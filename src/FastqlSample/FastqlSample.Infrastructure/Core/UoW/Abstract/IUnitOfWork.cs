using System;

namespace FastqlSample.Infrastructure.Core.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
