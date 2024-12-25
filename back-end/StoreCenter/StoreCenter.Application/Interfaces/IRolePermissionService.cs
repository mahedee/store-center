using StoreCenter.Domain.Entities;

namespace StoreCenter.Application.Interfaces
{
    public interface IRolePermissionService
    {
        Task<(bool Success, List<string> Errors, IEnumerable<RolePermission?> rolePermissions)> GetAllRolePermissionsAsync();
        //Task<(bool Success, List<string> Errors, RolePermission? permission)> GetRolePermissionByIdAsync(Guid permissionId);
        Task<(bool Success, List<string> Errors)> AddRolePermissionAsync(RolePermission rolePermission);
        //Task<(bool Success, List<string> Errors)> UpdatePermissionAsync(Permission permission);
        Task<(bool Success, List<string> Errors)> DeleteRolePermissionAsync(Guid roleId, Guid permissionId);
    }
}
