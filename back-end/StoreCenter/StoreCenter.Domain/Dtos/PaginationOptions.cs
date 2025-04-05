namespace StoreCenter.Domain.Dtos
{
    public class PaginationOptions
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string? SearchTerm { get; set; }
        public string? SearchField { get; set; }  // Name of the field to search by
        public string OrderBy { get; set; } = "Id";
        public bool IsDescending { get; set; } = false;
    }
}
