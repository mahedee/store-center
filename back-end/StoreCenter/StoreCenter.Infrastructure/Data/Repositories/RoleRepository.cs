using Microsoft.EntityFrameworkCore;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Infrastructure.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task AddRole(Role role)
        { 
           _context.Roles.Add(role);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Role?>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRole(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }
        public async Task<Role?> UpdateRole(Role role)
        {
            var existingRole = await _context.Roles.FindAsync(role.Id);
            if (existingRole == null)
            {
                return null;
            }

            // Update the existing role properties with the new role properties
            existingRole.Name = role.Name;
          
            _context.Roles.Update(existingRole);
            await _context.SaveChangesAsync();

            return existingRole;
        }
    }
}
