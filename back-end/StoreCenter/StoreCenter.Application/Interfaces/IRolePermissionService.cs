using StoreCenter.Application.Dtos;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;

namespace StoreCenter.Application.Interfaces
{
    public interface IRolePermissionService
    {
        Task<(bool Success, List<string> Errors, IEnumerable<RolePermissionDto?> rolePermissions)> GetAllRolePermissionsAsync();
        Task<(bool Success, List<string> Errors, RolePermission? rolePermission)> GetRolePermissionByIdAsync(Guid roleId, Guid permissionId);
        Task<(bool Success, List<string> Errors)> AddRolePermissionAsync(RolePermission rolePermission);
        Task<(bool Success, List<string> Errors)> AssignRolePermissionAsync(Guid roleId, Guid permissionId);
        //Task<(bool Success, List<string> Errors)> UpdatePermissionAsync(Permission permission);
        Task<(bool Success, List<string> Errors)> DeleteRolePermissionAsync(Guid roleId, Guid permissionId);
    }
}
