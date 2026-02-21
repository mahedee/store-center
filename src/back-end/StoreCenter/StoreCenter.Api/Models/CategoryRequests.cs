namespace StoreCenter.Api.Models
{
    public record CreateCategoryRequest
    {
        public required string Name { get; init; }
        public string Description { get; init; } = string.Empty;
    }

    public record UpdateCategoryRequest
    {
        public required string Name { get; init; }
        public string Description { get; init; } = string.Empty;
    }
}