using Microsoft.EntityFrameworkCore;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Infrastructure.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        public readonly ApplicationDbContext _context;
        public PermissionRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task AddPermission(Permission permission)
        { 
           _context.Permissions.Add(permission);
           await _context.SaveChangesAsync();
        }

        public async Task DeletePermission(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Permission?>> GetPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetPermission(Guid id)
        {
            return await _context.Permissions.FindAsync(id);
        }
        public async Task<Permission?> UpdatePermission(Permission permission)
        {
            var existingPermission = await _context.Permissions.FindAsync(permission.Id);
            if (existingPermission == null)
            {
                return null;
            }

            // Update the existing role properties with the new role properties
            existingPermission.Name = permission.Name;
          
            _context.Permissions.Update(existingPermission);
            await _context.SaveChangesAsync();

            return existingPermission;
        }
    }
}
