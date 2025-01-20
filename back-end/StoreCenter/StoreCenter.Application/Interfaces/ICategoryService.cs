using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;

namespace StoreCenter.Application.Interfaces
{
    public interface ICategoryService
    {
        //Task<(bool Success, List<string> Errors, IEnumerable<Category?> Categories)> GetAllCategoriesAsync();
        Task<PaginatedResultDto<Category>> GetAllCategoriesAsync(QueryParametersDto queryParametersDto);
        Task<(bool Success, List<string> Errors, Category? Category)> GetCategoryByIdAsync(Guid categoryId);
        Task<(bool Success, List<string> Errors)> AddCategoryAsync(Category category);
        Task<(bool Success, List<string> Errors)> UpdateCategoryAsync(Category category);
        Task<(bool Success, List<string> Errors)> DeleteCategoryAsync(Guid categoryId);
    }
}
