using Microsoft.EntityFrameworkCore;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;
using StoreCenter.Infrastructure.Data.Repositories.Base;

namespace StoreCenter.Infrastructure.Data.Repositories
{
    public class BrandRepository : Repository<Brand, Guid>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Brand?> GetByNameAsync(string name)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Brand>> GetBrandsWithProductsAsync()
        {
            return await _context.Brands
                .Include(b => b.Products)
                .ToListAsync();
        }
    }   
}