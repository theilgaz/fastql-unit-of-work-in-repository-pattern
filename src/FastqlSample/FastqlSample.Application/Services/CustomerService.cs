using FastqlSample.Application.Services.Base;
using FastqlSample.Domain.Entities;
using FastqlSample.Infrastructure.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastqlSample.Application.Services
{
    public class CustomerService : ServiceBase
    {
        public CustomerService(SqlConnectionConfiguration configuration) : base(configuration)
        {
        }

        public Customer GetCustomer(int id)
        {
            var response = uow.Customer.Get(id);
            if (response.IsSucceeded)
            {
                uow.Commit();
                return response.Entity;
            }
            else
            {
                uow.Rollback();
                return null;
            }
        }

        public long AddCustomer(Customer customer)
        {
            var response = uow.Customer.Insert(customer);
            if (response.IsSucceeded)
            {
                uow.Commit();
                return response.Entity;
            }
            else
            {
                uow.Rollback();
                return -1;
            }
        }

    }
}
