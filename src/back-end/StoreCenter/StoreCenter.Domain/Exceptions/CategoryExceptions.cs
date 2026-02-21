namespace StoreCenter.Domain.Exceptions
{
    public class CategoryNotFoundException : DomainException
    {
        public CategoryNotFoundException(Guid categoryId) 
            : base("CATEGORY_NOT_FOUND", $"Category with ID {categoryId} was not found.")
        {
        }
    }

    public class CategoryNameAlreadyExistsException : DomainException
    {
        public CategoryNameAlreadyExistsException(string name) 
            : base("CATEGORY_NAME_EXISTS", $"A category with name '{name}' already exists.")
        {
        }
    }

    public class InvalidCategoryDataException : DomainException
    {
        public InvalidCategoryDataException(string message) 
            : base("INVALID_CATEGORY_DATA", message)
        {
        }
    }
}