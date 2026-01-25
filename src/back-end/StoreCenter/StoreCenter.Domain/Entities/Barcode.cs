using StoreCenter.Domain.Entities.Base;

namespace StoreCenter.Domain.Entities
{
    public class Barcode : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public required string Code { get; set; } // The actual barcode value
        public string BarcodeType { get; set; } = "EAN13"; // EAN13, UPC, Code128, etc.
        public bool IsActive { get; set; } = true;
        public DateTime GeneratedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public Product? Product { get; set; }
    }
}