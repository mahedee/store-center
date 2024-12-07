using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace StoreCenter.Application.Services
{
    public class CategoryService : ICategoryService
    {

        public readonly ICategoryRepository _categoryRepository;
        public CategoryService( ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<(bool Success, List<string> Errors)> AddCategoryAsync(Category category)
        {
            var errors = new List<string>();
            try
            {
                await _categoryRepository.AddCategory(category);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors)> DeleteCategoryAsync(Guid categoryId)
        {
            var errors = new List<string>();
            try
            {
                await _categoryRepository.DeleteCategory(categoryId);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }

        public async Task<(bool Success, List<string> Errors, IEnumerable<Category?> Categories)> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetCategories();
                return (true, new List<string>(), categories);
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message }, Enumerable.Empty<Category?>());
            }
        }

        public Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return _categoryRepository.GetCategory(categoryId);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _categoryRepository.UpdateCategory(category);
        }
    }
}
