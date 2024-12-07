﻿using StoreCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category?>> GetCategories();
        Task<Category?> GetCategory(int id);
        Task AddCategory(Category category);
        Task<Category?> UpdateCategory(Category category);
        Task DeleteCategory(int id);
        Task SaveChangesAsync();
    }
}
