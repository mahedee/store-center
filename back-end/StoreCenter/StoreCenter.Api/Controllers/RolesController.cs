using Microsoft.AspNetCore.Mvc;
using StoreCenter.Api.Helpers;
using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _roleService.GetAllRolesAsync();
            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }
            return ApiResponseHelper.Success(result.roles, "Roles retrieved successfully");
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            if (!result.Success || result.role is null)
            {
                return ApiResponseHelper.NotFound("Role not found");
            }
            return ApiResponseHelper.Success(result.role);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Role role)
        {
            var result = await _roleService.AddRoleAsync(role);

            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }

            return ApiResponseHelper.Success(null, "Role created successfully");
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Role role)
        {
            if(id != role.Id)
            {
                return ApiResponseHelper.Error("Role ID mismatch");
            }

            var result = await _roleService.GetRoleByIdAsync(id);
            if (!result.Success || result.role is null)
            {
                return ApiResponseHelper.NotFound("Role not found");
            }

            role.Id = id;
            await _roleService.UpdateRoleAsync(role);
            return ApiResponseHelper.Success(null, "Role updated successfully");
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            if (!result.Success || result.role is null)
            {
                return ApiResponseHelper.NotFound("Role not found");
            }

            var deleteResult = await _roleService.DeleteRoleAsync(id);
            if (!deleteResult.Success)
            {
                return ApiResponseHelper.ValidationError(deleteResult.Errors);
            }

            return ApiResponseHelper.Success(null, "Role deleted successfully");
        }
    }
}
