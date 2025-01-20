using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    // Transaction Entity
    public class Transaction : BaseEntity<Guid>
    {
        public int ItemId { get; set; }
        public Product product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string TransactionType { get; set; } // e.g., "Purchase", "Sale", "Adjustment"
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
