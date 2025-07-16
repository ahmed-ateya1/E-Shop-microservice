using Ordering.Domain.Enum;

namespace Ordering.Application.Extensions
{
    public static class OrderExtension
    {
        public static IEnumerable<OrderDto> ToOrderDto(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderDto(
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                new AddressDto(
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.City,
                    order.ShippingAddress.State,
                    order.ShippingAddress.ZipCode
                ),
                new AddressDto(
                    order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.City,
                    order.BillingAddress.State,
                    order.BillingAddress.ZipCode
                ),
                new PaymentDto(
                    "",
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    order.Payment.PaymentMethod
                ),
                order.Status ?? OrderStatus.Draft,
                order.Items.Select(item => new OrderItemDto(
                    item.OrderId.Value,
                    item.ProductId.Value,
                    item.Quantity,
                    item.Price
                )).ToList()
            )).ToList();
        }
    }
}
