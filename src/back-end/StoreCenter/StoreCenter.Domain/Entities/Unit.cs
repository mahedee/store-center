using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class Unit : BaseEntity<Guid>
    {
        public required string Name { get; set; } // e.g., "Piece", "Kilogram", "Liter"
        public required string Symbol { get; set; } // e.g., "pcs", "kg", "L"
        public string? Description { get; set; }
        public string UnitType { get; set; } = "Count"; // Count, Weight, Volume, Length, etc.
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Product>? Products { get; set; }
    }
}