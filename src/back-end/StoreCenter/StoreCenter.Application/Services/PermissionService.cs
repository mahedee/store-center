using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Application.Services
{
    public class PermissionService : IPermissionService
    {

        public readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository) 
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<(bool Success, List<string> Errors)> AddPermissionAsync(Permission permission)
        {
            var errors = new List<string>();
            try
            {
                await _permissionRepository.AddPermission(permission);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors)> DeletePermissionAsync(Guid permissionId)
        {
            var errors = new List<string>();
            try
            {
                await _permissionRepository.DeletePermission(permissionId);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors, IEnumerable<Permission?> permissions)> GetAllPermissionsAsync()
        {
            try
            {
                var permissions = await _permissionRepository.GetPermissions();
                return (true, new List<string>(), permissions);
            }
            catch (Exception ex)
            {
                // Enumerable.Empty<Role?>() returns an empty collection of Role objects
                return (false, new List<string> { ex.Message }, Enumerable.Empty<Permission?>());
            }
        }

        public async Task<(bool Success, List<string> Errors, Permission? permission)>GetPermissionByIdAsync(Guid permissionId)
        {
            try
            {
                var permission = await _permissionRepository.GetPermission(permissionId);
                return (true, new List<string>(), permission);
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message }, null);
            }
        }

        public async Task<(bool Success, List<string> Errors)> UpdatePermissionAsync(Permission permission)
        {
            var errors = new List<string>();
            try
            {
                await _permissionRepository.UpdatePermission(permission);
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
