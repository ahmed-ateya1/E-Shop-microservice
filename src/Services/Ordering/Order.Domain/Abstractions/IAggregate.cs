namespace Ordering.Domain.Abstractions
{
    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
    public interface IAggregate<TID> : IAggregate, IEntity<TID>
    {
    }
}
