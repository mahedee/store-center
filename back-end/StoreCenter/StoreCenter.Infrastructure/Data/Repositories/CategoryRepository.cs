using Microsoft.EntityFrameworkCore;
using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
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

        //public async Task<IEnumerable<Category?>> GetCategories()
        //{
        //    return await _context.Categories.ToListAsync();
        //}


        public async Task<(IEnumerable<Category?> Categories, int Count)> GetCategories(QueryParametersDto queryParametersDto)
        {
            int pageSize = queryParametersDto.PageSize;
            int pageNumber = queryParametersDto.PageNumber;
            int offset = (pageNumber - 1) * pageSize;

            // Note: you can add search term and order by using AsQueryable() and Where() methods

            var totalRecords = await _context.Categories.CountAsync();
            var categories = await _context.Categories
                                           .Skip(offset)
                                           .Take(pageSize)
                                           .ToListAsync();
            return (categories, totalRecords);
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
