using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces.Base;

namespace StoreCenter.Infrastructure.Interfaces
{
    public interface IBrandRepository : IRepository<Brand, Guid>
    {
        Task<Brand?> GetByNameAsync(string name);
        Task<IEnumerable<Brand>> GetBrandsWithProductsAsync();
    }
}