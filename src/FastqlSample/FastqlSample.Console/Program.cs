using FastqlSample.Application.Services;
using FastqlSample.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FastqlSample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
             .AddSingleton<CustomerService>()
             .BuildServiceProvider();

            var service = serviceProvider.GetService<CustomerService>();

            var customer = service.GetCustomer(1);

            var newCustomer = new Customer()
            {
                Name = "Luke",
                Surname = "Skywalker",
                RegistrationNumber = "2",
            };

            var customerId = service.AddCustomer(newCustomer);

        }
    }
}
