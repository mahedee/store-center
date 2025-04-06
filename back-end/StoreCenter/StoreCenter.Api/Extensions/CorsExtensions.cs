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
                        policy.WithOrigins("http://localhost:3000")
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}
