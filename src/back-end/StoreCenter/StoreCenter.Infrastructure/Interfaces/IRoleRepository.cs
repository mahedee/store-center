using StoreCenter.Domain.Entities;

namespace StoreCenter.Infrastructure.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role?>> GetRoles();
        Task<Role?> GetRole(Guid id);
        Task AddRole(Role role);
        Task<Role?> UpdateRole(Role role);
        Task DeleteRole(Guid id);
    }
}
