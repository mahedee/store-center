using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class Brand : BaseEntity<Guid>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }

        // Navigation properties
        public ICollection<Product>? Products { get; set; }
    }
}