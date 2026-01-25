using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Infrastructure.Security
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        // If you want to check the user's permission from the database instead of the claim
        //, you can inject the IUserRepository here and use it to check the user's permission
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var userPermissions = context.User.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value);

            if (userPermissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
