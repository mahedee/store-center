using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        //public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Add this property to fix the error
        public ICollection<UserRole>? UserRoles { get; set; }
    }


}
