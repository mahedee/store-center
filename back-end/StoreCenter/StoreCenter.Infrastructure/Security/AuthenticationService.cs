using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;
using System.Security.Claims;

namespace StoreCenter.Infrastructure.Security
{
    public class AuthenticationService
    {
        IRolePermissionRepository _rolePermissionRepository;
        public AuthenticationService(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }
        public async Task<ClaimsPrincipal> CreatePrincipalAsync(User user, IEnumerable<string> userRoles)
        {
            var userPermissions = await FetchPermissionsForRoles(userRoles); // Assume this fetches permissions from DB

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(userPermissions.Select(p => new Claim("Permission", p)));

            var claimsIdentity = new ClaimsIdentity(claims, "CustomAuth");
            return new ClaimsPrincipal(claimsIdentity);
        }

        private async Task<IEnumerable<string>> FetchPermissionsForRoles(IEnumerable<string> roles)
        {
            // Fetch permissions from database or cache
            IEnumerable<RolePermissionDto?> rolePermissions = await _rolePermissionRepository.GetRolePermissions();

            // Get all permissions for the roles from the database
            List<string> permissions = new List<string>();
            foreach (var rp in rolePermissions)
            {
                if (rp != null && roles.Contains(rp.RoleName))
                {
                    permissions.Add(rp.PermissionName);
                }
            }

            return permissions.AsEnumerable();
        }
    }
}
