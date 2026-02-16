using StoreCenter.Domain.Entities.Base;
using StoreCenter.Domain.Exceptions;

namespace StoreCenter.Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        private string _name = string.Empty;

        public required string Name 
        { 
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidCategoryDataException("Category name cannot be empty.");
                
                if (value.Length > 100)
                    throw new InvalidCategoryDataException("Category name cannot exceed 100 characters.");

                _name = value.Trim();
            }
        }

        public string Description { get; set; } = string.Empty;

        // Business rule validation
        public void ValidateForCreation()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidCategoryDataException("Category name is required.");
        }

        public void ValidateForUpdate()
        {
            ValidateForCreation();
        }

        // ? is used to make the property nullable
        //public ICollection<Product> Products { get; set; }
    }
}
