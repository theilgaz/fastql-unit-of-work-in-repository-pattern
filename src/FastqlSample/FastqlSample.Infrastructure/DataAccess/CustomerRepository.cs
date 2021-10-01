using FastqlSample.Domain.Entities;
using FastqlSample.Infrastructure.Core.Repository;
using FastqlSample.Infrastructure.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FastqlSample.Infrastructure.DataAccess
{

    public interface ICustomerRepository : IGenericRepository<Customer>
    {

    }

    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbTransaction transaction) : base(transaction)
        {
        }

    }
}
