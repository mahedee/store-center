using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class Warehouse : BaseEntity<Guid>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? ContactPhone { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Stock>? Stocks { get; set; }
    }
}