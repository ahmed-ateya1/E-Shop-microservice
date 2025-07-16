using Ordering.Domain.Models;

namespace Ordering.Domain.Events
{
    public class OrderCreatedDomainEvent(Order order):IDomainEvent;
}
