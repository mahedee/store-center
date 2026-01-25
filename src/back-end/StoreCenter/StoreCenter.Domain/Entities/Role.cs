using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class Role : BaseEntity<Guid>
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Permission>? Permissions { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; } // Add this line to fix the error
    }

}
