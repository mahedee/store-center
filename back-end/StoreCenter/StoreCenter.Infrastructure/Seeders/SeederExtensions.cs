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
            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.SeedCategories();
            }

            return services;
        }
    }
}
