using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplicationApi.Application.Interfases;
using TaskApplicationApi.Application.Services;

namespace TaskApplicationApi.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddScoped<IAuthService,AuthService>();
            return services;
        }
    }
}
