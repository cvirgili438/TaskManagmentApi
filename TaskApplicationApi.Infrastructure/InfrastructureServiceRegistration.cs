using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Text;
using TaskApplicationApi.Infrastructure.Persistance;
using TaskApplicationApi.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using TaskApplicationApi.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TaskApplicationApi.Application.Contracts;
using TaskApplicationApi.Infrastructure.Repositories;
using TaskApplicationApi.Infrastructure.Security;

namespace TaskApplicationApi.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                   IConfiguration config) 
        {
            string connectionString = config.GetValue<string>("ConnectionStrings:DefaultConnection")!;
            ConfigurationHelper.DefaultConnection = connectionString;
            services.Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName));
            services.AddDbContext<UserDbContext>(options=> options.UseSqlServer(connectionString));
            services.AddIdentityCore<UserEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", opts =>
              {
                  var jwt = config.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
                  opts.TokenValidationParameters = new TokenValidationParameters
                  {

                      ValidIssuer = jwt.Issuer,
                      ValidAudience = jwt.Audience,
                      IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(jwt.Key)),

                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ClockSkew = TimeSpan.FromSeconds(30)
                  };
              });
            services.AddScoped<IRefreshTokenStore,RefreshTokenStore>();
            services.AddScoped<IAuthGateway,IdentityJwtAuthGateway>();
            services.AddScoped<IJwtProvider,JwtProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
