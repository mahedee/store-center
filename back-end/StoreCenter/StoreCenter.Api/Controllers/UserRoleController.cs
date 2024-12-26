using Microsoft.AspNetCore.Mvc;
using StoreCenter.Api.Helpers;
using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userRoleService.GetAllUserRolesAsync();
            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }
            return ApiResponseHelper.Success(result.userRoles, "UserRoles retrieved successfully");
        }

        

        [HttpPost("AssignUserRole")]
        public async Task<IActionResult> AssignUserRole([FromBody] UserRoleDto userRoleDto)
        {
            var result = await _userRoleService.AssignUserRoleAsync(userRoleDto.UserId, userRoleDto.RoleId);

            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }

            return ApiResponseHelper.Success(null, "RolePermission assigned successfully");
        }

        [HttpDelete("{userId}/{roleId}")]
        public async Task<IActionResult> Delete(Guid userId, Guid roleId)
        {
            var result = await _userRoleService.GetUserRoleByIdAsync(userId, roleId);
            if (!result.Success || result.userRole is null)
            {
                return ApiResponseHelper.NotFound("UserRole not found");
            }

            var deleteResult = await _userRoleService.DeleteUserRoleAsync(userId, roleId);
            if (!deleteResult.Success)
            {
                return ApiResponseHelper.ValidationError(deleteResult.Errors);
            }

            return ApiResponseHelper.Success(null, "UserRole deleted successfully");
        }
    }
}
