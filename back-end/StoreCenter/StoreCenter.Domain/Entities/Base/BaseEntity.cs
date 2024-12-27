namespace StoreCenter.Domain.Entities.Base
{
    public class BaseEntity<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
