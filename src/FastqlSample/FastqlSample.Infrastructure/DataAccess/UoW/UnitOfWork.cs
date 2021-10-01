using FastqlSample.Infrastructure.Core.UoW;
using System.Data.SqlClient;

namespace FastqlSample.Infrastructure.DataAccess.UoW
{
    public class UnitOfWork : UnitOfWorkBase
    {
        #region ctor
        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        #endregion

        private ICustomerRepository _customer;

        protected override void ResetRepositories()
        {
            _customer = null;
        }

        public ICustomerRepository Customer
        {
            get
            {
                return _customer ?? (_customer = new CustomerRepository(_transaction));
            }
        }

    }
}
