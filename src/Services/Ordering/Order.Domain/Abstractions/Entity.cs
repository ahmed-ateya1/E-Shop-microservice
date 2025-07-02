namespace Ordering.Domain.Abstractions
{
    public class Entity<TID> : IEntity<TID>
    {
        public TID Id { get; set; }
        public DateTime? CreateAt { get ; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
