﻿using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {

        private readonly IRolePermissionRepository _rolePermissionRepository;
        
        public RolePermissionService(IRolePermissionRepository rolePermissionRepository) 
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<(bool Success, List<string> Errors)> AssignRolePermissionAsync(Guid roleId, Guid permissionId)
        {
            var errors = new List<string>();
            try
            {
                await _rolePermissionRepository.AssignRolePermission(roleId, permissionId);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors)> AddRolePermissionAsync(RolePermission rolePermission)
        {
            var errors = new List<string>();
            try
            {
                await _rolePermissionRepository.AddRolePermission(rolePermission);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors)> DeleteRolePermissionAsync(Guid roleId, Guid permissionId)
        {
            var errors = new List<string>();
            try
            {
                await _rolePermissionRepository.DeleteRolePermission(roleId, permissionId);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors, IEnumerable<RolePermissionDto?> rolePermissions)> GetAllRolePermissionsAsync()
        {
            try
            {
                var rolePermissions = await _rolePermissionRepository.GetRolePermissions();
                return (true, new List<string>(), rolePermissions);
            }
            catch (Exception ex)
            {
                // Enumerable.Empty<Role?>() returns an empty collection of Role objects
                return (false, new List<string> { ex.Message }, Enumerable.Empty<RolePermissionDto?>());
            }
        }

        public async Task<(bool Success, List<string> Errors, RolePermission? rolePermission)> GetRolePermissionByIdAsync(Guid roleId, Guid permissionId)
        {
            try
            {
                var permission = await _rolePermissionRepository.GetRolePermission(roleId, permissionId);
                return (true, new List<string>(), permission);
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message }, null);
            }
        }

        //public async Task<(bool Success, List<string> Errors)> UpdatePermissionAsync(Permission permission)
        //{
        //    var errors = new List<string>();
        //    try
        //    {
        //        await _permissionRepository.UpdatePermission(permission);
        //        return (true, errors);
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex.Message);
        //        return (false, errors);
        //    }
        //}
    }
}
