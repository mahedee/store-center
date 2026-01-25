using Microsoft.EntityFrameworkCore;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            //return await _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            //return await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
            return user;
        }


        public async Task<IEnumerable<string>> GetUsersPermission(string userName)
        {
            //var userRoles = await _context.UserRoles.Where(ur => ur.UserId == user.Id).ToListAsync();
            //var roleIds = userRoles.Select(ur => ur.RoleId).ToList();
            //var rolePermissions = await _context.RolePermissions.Where(rp => roleIds.Contains(rp.RoleId)).ToListAsync();
            //var permissionIds = rolePermissions.Select(rp => rp.PermissionId).ToList();
            //var permissions = await _context.Permissions.Where(p => permissionIds.Contains(p.Id)).Select(p => p.Name).ToListAsync();

            var userPermissions = await (from user in _context.Users
                                         join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                         join rolePermission in _context.RolePermissions on userRole.RoleId equals rolePermission.RoleId
                                         join permission in _context.Permissions on rolePermission.PermissionId equals permission.Id
                                         where user.UserName == userName
                                         select permission.Name).ToListAsync();
            return userPermissions;
        }

        public async Task<UserPermissionDto> GetUserPermissionAsync(string userName)
        {
            var user = await GetUserByUserNameAsync(userName);

            if (user == null)
                return new UserPermissionDto();

            var userRoles = await (from ur in _context.UserRoles
                                   join r in _context.Roles on ur.RoleId equals r.Id
                                   where ur.UserId == user.Id
                                   select r.Name).ToListAsync();

            var userPermissions = await GetUsersPermission(userName);

            return new UserPermissionDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = userRoles,
                Permissions = userPermissions
            };
        }
    }
}
