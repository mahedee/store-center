using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Data;

namespace StoreCenter.Infrastructure.Seeders
{
    public static class RoleSeederExtensions
    {
        public static ApplicationDbContext SeedRoles(this ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return context;
            }

            var roles = new List<Role>
            {
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin"
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Manager"
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Staff"
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Supplier"
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer"
                }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
            return context;
        }
    }
}
