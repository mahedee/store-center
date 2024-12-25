using StoreCenter.Domain.Entities;

namespace StoreCenter.Infrastructure.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<RolePermission?>> GetRolePermissions();
        Task<RolePermission?> GetRolePermission(Guid roleId, Guid permissionId);
        Task AddRolePermission(RolePermission permission);
        Task AssignRolePermission(Guid roleId, Guid permissionId);
        //Task<Permission?> UpdatePermission(Permission permission);
        Task DeleteRolePermission(Guid roleId, Guid permissionId);
    }
}
