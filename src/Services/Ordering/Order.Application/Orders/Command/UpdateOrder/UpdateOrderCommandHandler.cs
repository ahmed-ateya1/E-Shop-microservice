namespace Ordering.Application.Orders.Command.UpdateOrder
{
    public class UpdateOrderCommandHandler(IApplicationDbContext db)
            : ICommandHandler<UpdateOrderCommand, UpdateOrderResultResponse>
    {
        public async Task<UpdateOrderResultResponse> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);

            var order = await db
                .Orders
                .FindAsync([orderId], cancellationToken);

            if (order == null)
            {
                throw new NotFoundException($"Order Not Found with Id {orderId.Value}");
            }

            UpdateOrder(order, command.Order);

            db.Orders.Update(order);

            await db.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResultResponse(order.Id.Value);
        }

        private void UpdateOrder(Order existingOrder, OrderDto order)
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

            existingOrder.Update(
                shippingAddress,
                billingAddress,
                payment,
                order.Status
            );
        }
    }
}
