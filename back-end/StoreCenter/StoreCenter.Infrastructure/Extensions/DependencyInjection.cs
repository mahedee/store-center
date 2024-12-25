using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreCenter.Infrastructure.Data.Repositories;
using StoreCenter.Infrastructure.Interfaces;
using StoreCenter.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
IConfiguration configuration, string _key, string _issuer, string _audience, string _expiryInMinutes)
        {
            services.AddSingleton<ITokenGenerator>(new TokenGenerator(_key, _issuer, _audience, _expiryInMinutes));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            return services;
        }
    }
}
