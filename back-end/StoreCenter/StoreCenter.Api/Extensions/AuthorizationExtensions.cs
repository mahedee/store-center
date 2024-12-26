using Microsoft.AspNetCore.Authorization;
using StoreCenter.Domain.Const;
using StoreCenter.Infrastructure.Security;

namespace StoreCenter.Api.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddPermissionBasedAuthorization(this IServiceCollection services)
        {
            // Add policies for permissions based authorization
            // Policies are used to define the permissions required to access a resource
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ActionPermissions.ViewReports, policy =>
                    policy.Requirements.Add(new PermissionRequirement(ActionPermissions.ViewReports)));

                options.AddPolicy(ActionPermissions.EditProfile, policy =>
                    policy.Requirements.Add(new PermissionRequirement(ActionPermissions.EditProfile)));

                options.AddPolicy(ActionPermissions.RolesViewAll, policy =>
                    policy.Requirements.Add(new PermissionRequirement(ActionPermissions.RolesViewAll)));

                options.AddPolicy(ActionPermissions.RolesViewById, policy =>
                    policy.Requirements.Add(new PermissionRequirement(ActionPermissions.RolesViewById)));

                options.AddPolicy(ActionPermissions.RolesCreate, policy =>
                    policy.Requirements.Add(new PermissionRequirement(ActionPermissions.RolesCreate)));

                options.AddPolicy(ActionPermissions.RolesUpdate, policy =>
                    policy.Requirements.Add(new PermissionRequirement(ActionPermissions.RolesUpdate)));

                options.AddPolicy(ActionPermissions.RolesDelete, policy =>
                    policy.Requirements.Add(new PermissionRequirement(ActionPermissions.RolesDelete)));

            });

            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            return services;
        }
    }
}
