using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

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

        //public async Task<(bool Success, List<string> Errors, IEnumerable<Category?> Categories)> GetAllCategoriesAsync()
        //{
        //    try
        //    {
        //        var categories = await _categoryRepository.GetCategories();
        //        return (true, new List<string>(), categories);
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, new List<string> { ex.Message }, Enumerable.Empty<Category?>());
        //    }
        //}

        public async Task<PaginatedResultDto<Category>> GetAllCategoriesAsync(QueryParametersDto queryParametersDto)
        {
            try
            {
                var (Categories, Count) = await _categoryRepository.GetCategories(queryParametersDto);
                return new PaginatedResultDto<Category>(queryParametersDto.PageNumber, queryParametersDto.PageSize, Count, true, new List<string>(), Categories);
            }
            catch (Exception ex)
            {
                return new PaginatedResultDto<Category>(queryParametersDto.PageNumber, queryParametersDto.PageSize, 0, false, new List<string> { ex.Message }, null);
            }
        }

        public async Task<(bool Success, List<string> Errors, Category? Category)> GetCategoryByIdAsync(Guid categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetCategory(categoryId);
                return (true, new List<string>(), category);
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message }, null);
            }
        }

        public async Task<(bool Success, List<string> Errors)> UpdateCategoryAsync(Category category)
        {
            var errors = new List<string>();
            try
            {
                await _categoryRepository.UpdateCategory(category);
                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return (false, errors);
            }
        }
    }
}
