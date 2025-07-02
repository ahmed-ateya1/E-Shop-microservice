namespace Ordering.Domain.Abstractions
{
    public interface IEntity
    {
        public DateTime? CreateAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public interface IEntity<TID> : IEntity
    {
        public TID Id { get; set; }
    }
}
