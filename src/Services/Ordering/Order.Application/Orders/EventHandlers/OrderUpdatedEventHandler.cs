using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
        : INotificationHandler<OrderUpdateDomainEvent>
    {
        public Task Handle(OrderUpdateDomainEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled: {Domain Event}",notification.GetType().Name);
            return Task.CompletedTask;
        }
    }

}
