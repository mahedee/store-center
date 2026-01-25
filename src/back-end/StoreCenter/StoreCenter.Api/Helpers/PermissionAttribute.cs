using Microsoft.AspNetCore.Authorization;

namespace StoreCenter.Api.Helpers
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
