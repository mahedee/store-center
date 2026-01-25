using StoreCenter.Domain.Dtos;
using StoreCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        Task<PaginatedResultDto<Category>> GetCategories(PaginationOptions paginationOptions);
        Task<Category?> GetCategory(Guid id);
        Task AddCategory(Category category);
        Task<Category?> UpdateCategory(Category category);
        Task DeleteCategory(Guid id);
    }
}
