using FastqlSample.Infrastructure.Core.Configurations;
using FastqlSample.Infrastructure.DataAccess.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastqlSample.Application.Services.Base
{
    public class ServiceBase
    {
        internal UnitOfWork uow;

        private readonly SqlConnectionConfiguration _configuration;
        public ServiceBase(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
            uow = new UnitOfWork(_configuration.Value);
        }

        public void ChangeDatabase(SqlConnectionConfiguration configuration)
        {
            uow = new UnitOfWork(configuration.Value);
        }
    }
}
