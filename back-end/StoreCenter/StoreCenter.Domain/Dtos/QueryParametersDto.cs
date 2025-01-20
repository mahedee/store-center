namespace StoreCenter.Domain.Dtos
{
    public class QueryParametersDto
    {
    
        public int PageNumber { get; set; } = 1; // Default page number
        public int PageSize { get; set; } = 50;  // Default page size
        //public string SearchTerm { get; set; } // Optional search term
        //public string OrderBy { get; set; } = "Id"; // Default sorting field
        //public bool IsDescending { get; set; } = false; // Default sorting order
    }
}
