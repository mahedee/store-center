using StoreCenter.Domain.Entities;

namespace StoreCenter.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<(bool Success, List<string> Errors, IEnumerable<Permission?> permissions)> GetAllPermissionsAsync();
        Task<(bool Success, List<string> Errors, Permission? permission)> GetPermissionByIdAsync(Guid permissionId);
        Task<(bool Success, List<string> Errors)> AddPermissionAsync(Permission permission);
        Task<(bool Success, List<string> Errors)> UpdatePermissionAsync(Permission permission);
        Task<(bool Success, List<string> Errors)> DeletePermissionAsync(Guid permissionId);
    }
}
