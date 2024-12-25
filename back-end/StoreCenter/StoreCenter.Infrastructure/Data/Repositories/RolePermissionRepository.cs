using Microsoft.EntityFrameworkCore;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Infrastructure.Data.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        public readonly ApplicationDbContext _context;
        public RolePermissionRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task AddRolePermission(RolePermission rolePermission)
        { 
           _context.RolePermissions.Add(rolePermission);
           await _context.SaveChangesAsync();
        }

        public async Task AssignRolePermission(Guid roleId, Guid permissionId)
        {
            //Assign the role permission to the database using the roleId and permissionId

            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            _context.RolePermissions.Add(rolePermission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRolePermission(Guid roleId, Guid permissionId)
        {
            //Remove the role permission from the database using the roleId and permissionId

            var rolePermission = await _context.RolePermissions.FindAsync(roleId, permissionId);
            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RolePermission?>> GetRolePermissions()
        {
            return await _context.RolePermissions
                                 .Include(rp => rp.Role)
                                 .Include(rp => rp.Permission)
                                 .ToListAsync();
        }

        public async Task<RolePermission?> GetRolePermission(Guid roleId, Guid permissionId)
        {
            return await _context.RolePermissions.FindAsync(roleId, permissionId);
        }

        //public async Task<Permission?> UpdatePermission(Permission permission)
        //{
        //    var existingPermission = await _context.Permissions.FindAsync(permission.Id);
        //    if (existingPermission == null)
        //    {
        //        return null;
        //    }

        //    // Update the existing role properties with the new role properties
        //    existingPermission.Name = permission.Name;

        //    _context.Permissions.Update(existingPermission);
        //    await _context.SaveChangesAsync();

        //    return existingPermission;
        //}
    }
}
