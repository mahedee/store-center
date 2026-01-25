using StoreCenter.Application.Dtos;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;

namespace StoreCenter.Application.Interfaces
{
    public interface IUserRoleService
    {
        Task<(bool Success, List<string> Errors, IEnumerable<UserRoleDto?> userRoles)> GetAllUserRolesAsync();
        Task<(bool Success, List<string> Errors, UserRole? userRole)> GetUserRoleByIdAsync(Guid userId, Guid roleId);
        Task<(bool Success, List<string> Errors)> AssignUserRoleAsync(Guid userId, Guid roleId);
        Task<(bool Success, List<string> Errors)> DeleteUserRoleAsync(Guid userId, Guid roleId);
    }
}
