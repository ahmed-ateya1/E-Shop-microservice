namespace Ordering.Domain.Events
{
    public class OrderCreatedDomainEvent(Models.Order order):IDomainEvent;
}
