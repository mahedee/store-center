using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class Sale : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; } // Sales person
        public string SaleNumber { get; set; } = Guid.NewGuid().ToString();
        public DateTime SaleDate { get; set; } = DateTime.Now;
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Completed"; // Completed, Pending, Cancelled, Refunded
        public string? Notes { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; // Cash, Card, Bank Transfer, etc.

        // Navigation properties
        public Customer? Customer { get; set; }
        public User? User { get; set; }
        public ICollection<SaleItem>? SaleItems { get; set; }
    }
}