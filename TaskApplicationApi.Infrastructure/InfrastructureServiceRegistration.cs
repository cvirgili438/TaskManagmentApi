using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Infrastructure.Persistance;

namespace TaskApplicationApi.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                   IConfiguration configuration) 
        {
            string connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection")!;
            ConfigurationHelper.DefaultConnection = connectionString;
            return services;
        }
    }
}
