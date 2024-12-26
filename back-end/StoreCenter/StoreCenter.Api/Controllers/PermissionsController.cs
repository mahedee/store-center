using Microsoft.AspNetCore.Mvc;
using StoreCenter.Api.Helpers;
using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _permissionService.GetAllPermissionsAsync();
            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }
            return ApiResponseHelper.Success(result.permissions, "Permission retrieved successfully");
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _permissionService.GetPermissionByIdAsync(id);
            if (!result.Success || result.permission is null)
            {
                return ApiResponseHelper.NotFound("Permission not found");
            }
            return ApiResponseHelper.Success(result.permission);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Permission permission)
        {
            var result = await _permissionService.AddPermissionAsync(permission);

            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }

            return ApiResponseHelper.Success(null, "Permission created successfully");
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Permission permission)
        {
            if(id != permission.Id)
            {
                return ApiResponseHelper.Error("Permission ID mismatch");
            }

            var result = await _permissionService.GetPermissionByIdAsync(id);
            if (!result.Success || result.permission is null)
            {
                return ApiResponseHelper.NotFound("Permission not found");
            }

            permission.Id = id;
            await _permissionService.UpdatePermissionAsync(permission);
            return ApiResponseHelper.Success(null, "Permission updated successfully");
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _permissionService.GetPermissionByIdAsync(id);
            if (!result.Success || result.permission is null)
            {
                return ApiResponseHelper.NotFound("Permission not found");
            }

            var deleteResult = await _permissionService.DeletePermissionAsync(id);
            if (!deleteResult.Success)
            {
                return ApiResponseHelper.ValidationError(deleteResult.Errors);
            }

            return ApiResponseHelper.Success(null, "Permission deleted successfully");
        }
    }
}
