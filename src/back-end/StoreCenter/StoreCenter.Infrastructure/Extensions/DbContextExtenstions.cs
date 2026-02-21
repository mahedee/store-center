using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreCenter.Infrastructure.Data;

namespace StoreCenter.Infrastructure.Extensions
{
    public static class DbContextExtenstions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services,
            IConfiguration configuration, IHostEnvironment? environment = null)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Temporarily force in-memory database for all environments to get API running
                options.UseInMemoryDatabase("StoreCenterInMemoryDb");
                
                // TODO: Uncomment this for production with proper SQL Server setup
                // if (environment?.IsDevelopment() == true)
                // {
                //     options.UseInMemoryDatabase("StoreCenterInMemoryDb");
                // }
                // else
                // {
                //     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                //         b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                // }
            });

            return services;
        }
    }
}
