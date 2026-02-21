using Microsoft.Extensions.DependencyInjection;
using StoreCenter.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Infrastructure.Seeders
{
    public static class SeederExtensions
    {
        public static IServiceCollection AddSeedData(this IServiceCollection services)
        {
            // Don't seed data during service registration - this will be done at startup instead
            // using (var serviceScope = services.BuildServiceProvider().CreateScope())
            // {
            //     var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            //     context?.SeedCategories();
            //     context?.SeedRoles();
            //     context?.SeedBrands();
            // }

            return services;
        }
        
        public static void SeedDatabaseData(this IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context?.SeedCategories();
                context?.SeedRoles();
                context?.SeedBrands();
            }
        }
    }
}
