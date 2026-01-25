using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Application.Services
{
    public class RoleService : IRoleService
    {

        public readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository) 
        {
            _roleRepository = roleRepository;
        }
        public async Task<(bool Success, List<string> Errors)> AddRoleAsync(Role role)
        {
            var errors = new List<string>();
            try
            {
                await _roleRepository.AddRole(role);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors)> DeleteRoleAsync(Guid roleId)
        {
            var errors = new List<string>();
            try
            {
                await _roleRepository.DeleteRole(roleId);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors, IEnumerable<Role?> roles)> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetRoles();
                return (true, new List<string>(), roles);
            }
            catch (Exception ex)
            {
                // Enumerable.Empty<Role?>() returns an empty collection of Role objects
                return (false, new List<string> { ex.Message }, Enumerable.Empty<Role?>());
            }
        }

        public async Task<(bool Success, List<string> Errors, Role? role)>GetRoleByIdAsync(Guid roleId)
        {
            try
            {
                var role = await _roleRepository.GetRole(roleId);
                return (true, new List<string>(), role);
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message }, null);
            }
        }

        public async Task<(bool Success, List<string> Errors)> UpdateRoleAsync(Role role)
        {
            var errors = new List<string>();
            try
            {
                await _roleRepository.UpdateRole(role);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }
    }
}
