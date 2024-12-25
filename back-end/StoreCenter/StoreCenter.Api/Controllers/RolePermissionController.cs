using Microsoft.AspNetCore.Mvc;
using StoreCenter.Api.Helpers;
using StoreCenter.Application.Dtos;
using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Entities;
using System.Text.Json.Serialization;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionsController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionsController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _rolePermissionService.GetAllRolePermissionsAsync();
            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }
            return ApiResponseHelper.Success(result.rolePermissions, "RolePermission retrieved successfully");

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    WriteIndented = true
            //};
            //var json = JsonSerializer.Serialize(result.rolePermissions, options);
            //return new JsonResult(json);
        }

        //// GET api/<CategoriesController>/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var result = await _permissionService.GetPermissionByIdAsync(id);
        //    if (!result.Success || result.permission is null)
        //    {
        //        return ApiResponseHelper.NotFound("Permission not found");
        //    }
        //    return ApiResponseHelper.Success(result.permission);
        //}

        // POST api/<CategoriesController>
        [HttpPost("AddRolePermission")]
        public async Task<IActionResult> Post([FromBody] RolePermission rolePermission)
        {
            var result = await _rolePermissionService.AddRolePermissionAsync(rolePermission);

            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }

            return ApiResponseHelper.Success(null, "RolePermission created successfully");
        }

        [HttpPost("AssignRolePermission")]
        public async Task<IActionResult> AssignRolePermission([FromBody] RolePermissionDto rolePermissionDto)
        {
            var result = await _rolePermissionService.AssignRolePermissionAsync(rolePermissionDto.RoleId, rolePermissionDto.PermissionId);

            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }

            return ApiResponseHelper.Success(null, "RolePermission assigned successfully");
        }

        //// PUT api/<CategoriesController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(Guid id, [FromBody] Permission permission)
        //{
        //    if(id != permission.Id)
        //    {
        //        return ApiResponseHelper.Error("Permission ID mismatch");
        //    }

        //    var result = await _permissionService.GetPermissionByIdAsync(id);
        //    if (!result.Success || result.permission is null)
        //    {
        //        return ApiResponseHelper.NotFound("Permission not found");
        //    }

        //    permission.Id = id;
        //    await _permissionService.UpdatePermissionAsync(permission);
        //    return ApiResponseHelper.Success(null, "Permission updated successfully");
        //}

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid roleId, Guid permissionId)
        {
            var result = await _rolePermissionService.GetRolePermissionByIdAsync(roleId, permissionId);
            if (!result.Success || result.rolePermission is null)
            {
                return ApiResponseHelper.NotFound("RolePermission not found");
            }

            var deleteResult = await _rolePermissionService.DeleteRolePermissionAsync(roleId, permissionId);
            if (!deleteResult.Success)
            {
                return ApiResponseHelper.ValidationError(deleteResult.Errors);
            }

            return ApiResponseHelper.Success(null, "Permission deleted successfully");
        }
    }
}
