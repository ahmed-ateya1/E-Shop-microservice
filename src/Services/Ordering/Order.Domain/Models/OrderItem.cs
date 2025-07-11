﻿namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public ProductId ProductId { get; set; }
        public OrderId OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
