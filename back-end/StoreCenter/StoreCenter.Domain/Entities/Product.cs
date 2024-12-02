namespace StoreCenter.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        // required keyword is used to make the property non-nullable
        public required string Name { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // ? is used to make the property nullable
        public Category Category { get; set; }
    }

}
