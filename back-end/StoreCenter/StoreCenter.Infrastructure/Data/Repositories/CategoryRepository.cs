using Microsoft.EntityFrameworkCore;
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
           await _context.Categories.AddAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                //await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category?>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public Task<Category?> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category?> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
