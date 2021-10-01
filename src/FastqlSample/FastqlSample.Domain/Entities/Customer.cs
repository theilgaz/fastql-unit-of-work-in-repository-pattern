using Fastql.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastqlSample.Domain.Entities
{
    [Table("Customer","Sales")]
    public class Customer
    {
        [IsPrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [IsNotUpdatable]
        public string RegistrationNumber { get; set; }

    }
}
