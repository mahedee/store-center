using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    // InventoryAlert Entity
    public class InventoryAlert : BaseEntity<Guid>
    {
        public int ItemId { get; set; }
        public Product Product { get; set; }
        public int Threshold { get; set; }
        public string AlertMessage { get; set; }
    }
}
