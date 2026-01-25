using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class Stock : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
        public int MinimumThreshold { get; set; }
        public int MaximumCapacity { get; set; }
        public string? Location { get; set; } // Specific location within warehouse (e.g., "Aisle 3, Shelf B")
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // Navigation properties
        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}