using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;

namespace StoreCenter.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<PaginatedResultDto<Category>> GetAllCategoriesAsync(PaginationOptions paginationOptions);
        Task<(bool Success, List<string> Errors, Category? Category)> GetCategoryByIdAsync(Guid categoryId);
        Task<(bool Success, List<string> Errors)> AddCategoryAsync(Category category);
        Task<(bool Success, List<string> Errors)> UpdateCategoryAsync(Category category);
        Task<(bool Success, List<string> Errors)> DeleteCategoryAsync(Guid categoryId);
    }
}
