using StoreCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<(bool Success, List<string> Errors, IEnumerable<Category?> Categories)> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task<(bool Success, List<string> Errors)> AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task<(bool Success, List<string> Errors)> DeleteCategoryAsync(Guid categoryId);
    }
}
