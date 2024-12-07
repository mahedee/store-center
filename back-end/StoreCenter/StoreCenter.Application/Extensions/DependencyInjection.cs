﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreCenter.Application.Interfaces;
using StoreCenter.Application.Services;

namespace StoreCenter.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
IConfiguration configuration)
        {
           services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
           services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
