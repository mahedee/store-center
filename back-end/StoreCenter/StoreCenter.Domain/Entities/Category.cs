namespace StoreCenter.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }

        // ? is used to make the property nullable
        //public ICollection<Product> Products { get; set; }
    }

}
