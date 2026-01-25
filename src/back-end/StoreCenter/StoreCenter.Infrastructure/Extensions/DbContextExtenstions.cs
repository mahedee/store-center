using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreCenter.Infrastructure.Data;

namespace StoreCenter.Infrastructure.Extensions
{
    public static class DbContextExtenstions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            return services;

        }
    }
}
