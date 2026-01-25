using Microsoft.EntityFrameworkCore;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Extensions;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task AddCategory(Category category)
        { 
           _context.Categories.Add(category);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }


        // Refactored method to handle pagination, filtering, and sorting
        public async Task<PaginatedResultDto<Category>> GetCategories(PaginationOptions paginationOptions)
        {
            IQueryable<Category> query = _context.Categories;

            // Apply pagination, sorting, and search logic using the extension method
            query = query.ApplyPagination(paginationOptions);

            // Get the total count after filtering and pagination
            int totalCount = await query.CountAsync();

            // Get the result set (categories after applying pagination)
            var categories = await query.ToListAsync();

            // Return PaginatedResultDto containing the paginated data
            return new PaginatedResultDto<Category>(
                paginationOptions.PageNumber,
                paginationOptions.PageSize,
                totalCount,
                true,
                new List<string>(),  // Errors, if any
                categories
            );
        }

        public async Task<Category?> GetCategory(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<Category?> UpdateCategory(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.Id);
            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;
        }
    }
}
