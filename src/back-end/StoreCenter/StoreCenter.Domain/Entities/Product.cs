using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    // Inventory Item
    public class Product : BaseEntity<Guid>
    {
        //public Guid Id { get; set; }

        // required keyword is used to make the property non-nullable
        public required string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // ? is used to make the property nullable
        public Category Category { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string UnitOfMeasurement { get; set; } // e.g. kg, g, l, ml, etc.

        //public ICollection<InventoryAlert> InventoryAlerts { get; set; }
        //public ICollection<Transaction> Transactions { get; set; }
    }

}
