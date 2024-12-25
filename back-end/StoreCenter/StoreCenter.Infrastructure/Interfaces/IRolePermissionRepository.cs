using StoreCenter.Domain.Entities;

namespace StoreCenter.Infrastructure.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<IEnumerable<RolePermission?>> GetRolePermissions();
        //Task<RolePermission?> GetRolePermission(Guid id);
        Task AddRolePermission(RolePermission permission);
        //Task<Permission?> UpdatePermission(Permission permission);
        Task DeleteRolePermission(Guid roleId, Guid permissionId);
    }
}
