using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedDomainEvent>
    {
        public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
