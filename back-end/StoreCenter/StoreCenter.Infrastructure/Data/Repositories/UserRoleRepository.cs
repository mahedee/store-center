using Microsoft.EntityFrameworkCore;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Infrastructure.Data.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        public readonly ApplicationDbContext _context;
        public UserRoleRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        
        public async Task AssignUserRole(Guid userId, Guid roleId)
        {
            //Assign the role permission to the database using the roleId and permissionId

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserRole(Guid userId, Guid roleId)
        {
            var userRole = await _context.UserRoles.FindAsync(userId, roleId);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserRoleDto?>> GetUserRoles()
        {

            return await _context.UserRoles
                .Select(ur => new UserRoleDto
                {
                    UserId = ur.User.Id,
                    UserName = ur.User.UserName,
                    RoleId = ur.Role.Id,
                    RoleName = ur.Role.Name
                })
                .ToListAsync();
        }

        public async Task<UserRole?> GetUserRole(Guid userId, Guid roleId)
        {
            return await _context.UserRoles.FindAsync(userId, roleId);
        }
    }
}
