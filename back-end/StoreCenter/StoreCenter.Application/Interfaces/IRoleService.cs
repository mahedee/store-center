using StoreCenter.Domain.Entities;

namespace StoreCenter.Application.Interfaces
{
    public interface IRoleService
    {
        Task<(bool Success, List<string> Errors, IEnumerable<Role?> roles)> GetAllRolesAsync();
        Task<(bool Success, List<string> Errors, Role? role)> GetRoleByIdAsync(Guid roleId);
        Task<(bool Success, List<string> Errors)> AddRoleAsync(Role role);
        Task<(bool Success, List<string> Errors)> UpdateRoleAsync(Role role);
        Task<(bool Success, List<string> Errors)> DeleteRoleAsync(Guid roleId);
    }
}
