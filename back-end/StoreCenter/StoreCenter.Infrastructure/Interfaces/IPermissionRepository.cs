using StoreCenter.Domain.Entities;

namespace StoreCenter.Infrastructure.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission?>> GetPermissions();
        Task<Permission?> GetPermission(Guid id);
        Task AddPermission(Permission permission);
        Task<Permission?> UpdatePermission(Permission permission);
        Task DeletePermission(Guid id);
    }
}
