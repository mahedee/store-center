using Microsoft.AspNetCore.Authorization;

namespace StoreCenter.Application.Helper
{
    public class PermissionRequirement_obsolete : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement_obsolete(string permission)
        {
            Permission = permission;
        }
    }
}
