using Microsoft.AspNetCore.Authorization;
using StoreCenter.Domain.Const;
using StoreCenter.Infrastructure.Security;

namespace StoreCenter.Api.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins(
                                "http://localhost:3000",
                                "http://localhost:3001", 
                                "http://localhost:3002",
                                "http://localhost:5173", // Vite dev server default
                                "http://localhost:4200", // Angular dev server default
                                "http://localhost:8080", // Vue dev server default
                                "https://localhost:3000",
                                "https://localhost:3001",
                                "https://localhost:5173"
                             )
                             .AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowCredentials(); // Allow credentials for authentication
                    });
            });

            return services;
        }
    }
}
