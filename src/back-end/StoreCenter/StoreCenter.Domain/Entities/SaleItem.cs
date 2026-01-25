using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class SaleItem : BaseEntity<Guid>
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
        public decimal TotalPrice { get; set; }

        // Navigation properties
        public Sale? Sale { get; set; }
        public Product? Product { get; set; }
    }
}