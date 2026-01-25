using StoreCenter.Domain.Dtos;

namespace StoreCenter.Application.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, List<string> Errors, IEnumerable<UserDto?> userDtos)> GetAllUsersAsync();
        Task<IEnumerable<string>> GetUsersPermission(string userName);
        Task<UserPermissionDto> GetUserPermissionAsync(string userName);
        //Task<(bool Success, List<string> Errors, Category? Category)> GetCategoryByIdAsync(Guid categoryId);
        //Task<(bool Success, List<string> Errors)> AddCategoryAsync(Category category);
        //Task<(bool Success, List<string> Errors)> UpdateCategoryAsync(Category category);
        //Task<(bool Success, List<string> Errors)> DeleteCategoryAsync(Guid categoryId);
    }
}
