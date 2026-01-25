using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    // Supplier Entity
    public class Supplier : BaseEntity<Guid>
    {
        public required string Name { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
    }
}
