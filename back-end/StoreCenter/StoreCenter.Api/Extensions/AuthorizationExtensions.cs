using Microsoft.AspNetCore.Authorization;
using StoreCenter.Domain.Const;
using StoreCenter.Infrastructure.Security;

namespace StoreCenter.Api.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddPermissionBasedAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.ViewReports, policy =>
                    policy.Requirements.Add(new PermissionRequirement(Permissions.ViewReports)));

                options.AddPolicy(Permissions.EditProfile, policy =>
                    policy.Requirements.Add(new PermissionRequirement(Permissions.EditProfile)));
            });

            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            return services;
        }
    }
}
