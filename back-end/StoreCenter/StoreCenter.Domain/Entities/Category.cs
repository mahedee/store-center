using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        //public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }

        // ? is used to make the property nullable
        //public ICollection<Product> Products { get; set; }
    }

}
