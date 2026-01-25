using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    // PurchaseOrder Entity
    public class PurchaseOrder : BaseEntity<Guid>
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
