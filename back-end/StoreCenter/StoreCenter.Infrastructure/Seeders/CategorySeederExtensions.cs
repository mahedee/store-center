using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Infrastructure.Seeders
{
    public static class CategorySeederExtensions
    {
        public static ApplicationDbContext SeedCategories(this ApplicationDbContext context)
        {
            if (context.Categories.Any())
            {
                return context;
            }

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Electronics",
                    Description = "Electronic gadgets and accessories"
                },
                new Category
                {
                    Name = "Clothing",
                    Description = "Fashionable clothing items"
                },
                new Category
                {
                    Name = "Books",
                    Description = "Educational and fictional books"
                },
                new Category
                {
                    Name = "Furniture",
                    Description = "Home and office furniture"
                },
                new Category
                {
                    Name = "Appliances",
                    Description = "Home and kitchen appliances"
                }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
            return context;
        }
    }
}
