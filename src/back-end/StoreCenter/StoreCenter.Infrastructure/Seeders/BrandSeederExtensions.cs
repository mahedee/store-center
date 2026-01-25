using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Infrastructure.Seeders
{
    public static class BrandSeederExtensions
    {
        public static ApplicationDbContext SeedBrands(this ApplicationDbContext context)
        {
            if (context.Brands.Any())
            {
                return context;
            }

            var brands = new List<Brand>
            {
                new Brand
                {
                    Name = "Apple",
                    Description = "Premium technology and consumer electronics",
                    Website = "https://www.apple.com",
                    LogoUrl = "https://www.apple.com/favicon.ico"
                },
                new Brand
                {
                    Name = "Samsung",
                    Description = "Global technology leader in electronics",
                    Website = "https://www.samsung.com",
                    LogoUrl = "https://www.samsung.com/favicon.ico"
                },
                new Brand
                {
                    Name = "Nike",
                    Description = "Athletic footwear and apparel",
                    Website = "https://www.nike.com",
                    LogoUrl = "https://www.nike.com/favicon.ico"
                },
                new Brand
                {
                    Name = "Sony",
                    Description = "Electronics, gaming, and entertainment",
                    Website = "https://www.sony.com",
                    LogoUrl = "https://www.sony.com/favicon.ico"
                },
                new Brand
                {
                    Name = "Microsoft",
                    Description = "Software, cloud computing, and technology",
                    Website = "https://www.microsoft.com",
                    LogoUrl = "https://www.microsoft.com/favicon.ico"
                },
                new Brand
                {
                    Name = "Adidas",
                    Description = "Sports clothing and accessories",
                    Website = "https://www.adidas.com",
                    LogoUrl = "https://www.adidas.com/favicon.ico"
                },
                new Brand
                {
                    Name = "HP",
                    Description = "Personal computers and printers",
                    Website = "https://www.hp.com",
                    LogoUrl = "https://www.hp.com/favicon.ico"
                },
                new Brand
                {
                    Name = "Dell",
                    Description = "Computer technology and services",
                    Website = "https://www.dell.com",
                    LogoUrl = "https://www.dell.com/favicon.ico"
                }
            };

            context.Brands.AddRange(brands);
            context.SaveChanges();
            return context;
        }
    }
}