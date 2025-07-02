namespace Ordering.Domain.Events
{
    public record OrderUpdateDomainEvent(Models.Order Order):IDomainEvent;
}
