using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;

namespace StoreCenter.Infrastructure.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRoleDto?>> GetUserRoles();
        Task<UserRole?> GetUserRole(Guid userId, Guid roleId);
        Task AssignUserRole(Guid userId, Guid roleId);
        Task DeleteUserRole(Guid userId, Guid roleId);
    }
}
