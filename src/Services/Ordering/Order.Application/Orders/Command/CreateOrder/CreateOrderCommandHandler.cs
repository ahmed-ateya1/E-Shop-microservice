namespace Ordering.Application.Orders.Command.CreateOrder
{
    public class CreateOrderCommandHandler(IApplicationDbContext db)
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = createOrder(command.Order);

            db.Orders.Add(order);
            await db.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);

        }
        private Order createOrder(OrderDto order)
        {
            var shippingAddress = Address.Of(
                order.ShippingAddress.FirstName,
                order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress,
                order.ShippingAddress.AddressLine,
                order.ShippingAddress.City,
                order.ShippingAddress.State,
                order.ShippingAddress.ZipCode
            );

            var billingAddress = Address.Of(
                order.BillingAddress.FirstName,
                order.BillingAddress.LastName,
                order.BillingAddress.EmailAddress,
                order.BillingAddress.AddressLine,
                order.BillingAddress.City,
                order.BillingAddress.State,
                order.BillingAddress.ZipCode
            );

            var payment = Payment.Of(
                order.Payment.CardNumber,
                order.Payment.Expiration,
                order.Payment.Cvv,
                order.Payment.PaymentMethod
            );

            var orderId = OrderId.Of(Guid.NewGuid());
            var customerId = CustomerId.Of(order.CustomerId);
            var orderName = OrderName.Of(order.OrderName);

            var newOrder = Order.Create(
                orderId,
                customerId,
                orderName,
                shippingAddress,
                billingAddress,
                payment
            );

            foreach (var itemDto in order.OrderItems)
            {
                var productId = ProductId.Of(itemDto.ProductId);
                newOrder.AddOrderItem(productId, itemDto.Quantity, itemDto.Price);
            }

            return newOrder;
        }
    }
}
