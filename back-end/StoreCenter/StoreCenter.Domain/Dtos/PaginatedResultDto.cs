namespace StoreCenter.Domain.Dtos
{
    public class PaginatedResultDto<TEntity> where TEntity : class
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        // Total number of records
        public long Count { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPrevious => CurrentPage > 1 && TotalPages > 1;

        public bool HasNext => CurrentPage < TotalPages;
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }

        public IEnumerable<TEntity> Results { get; set; }

        public PaginatedResultDto(int currentPage, int pageSize, long count, bool success, List<string> errors, IEnumerable<TEntity>? results)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            Count = count;
            Success = success;
            Errors = errors;
            Results = results;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
