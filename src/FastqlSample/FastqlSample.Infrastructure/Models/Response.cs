using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastqlSample.Infrastructure.Models
{
    public class Response<TEntity>
    {
        public TEntity Entity { get; set; }

        public bool IsSucceeded { get; set; }

        public string ResponseMessage { get; set; }
    }
}
