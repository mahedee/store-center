using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Application.Services
{
    public class UserRoleService : IUserRoleService
    {

        private readonly IUserRoleRepository _userRoleRepository;
        
        public UserRoleService(IUserRoleRepository roleRepository) 
        {
            _userRoleRepository = roleRepository;
        }

        public async Task<(bool Success, List<string> Errors)> AssignUserRoleAsync(Guid userId, Guid roleId)
        {

            var errors = new List<string>();
            try
            {
                await _userRoleRepository.AssignUserRole(userId, roleId);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors)> DeleteUserRoleAsync(Guid userId, Guid roleId)
        {
            var errors = new List<string>();
            try
            {
                await _userRoleRepository.DeleteUserRole(userId, roleId);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors, IEnumerable<UserRoleDto?> userRoles)> GetAllUserRolesAsync()
        {
            try
            {
                var userRoles = await _userRoleRepository.GetUserRoles();
                return (true, new List<string>(), userRoles);
            }
            catch (Exception ex)
            {
                // Enumerable.Empty<Role?>() returns an empty collection of Role objects
                return (false, new List<string> { ex.Message }, Enumerable.Empty<UserRoleDto?>());
            }
        }

        public async Task<(bool Success, List<string> Errors, UserRole? userRole)> GetUserRoleByIdAsync(Guid userId, Guid roleId)
        {
            try
            {
                var userRole = await _userRoleRepository.GetUserRole(userId, roleId);
                return (true, new List<string>(), userRole);
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message }, null);
            }
        }
    }
}
