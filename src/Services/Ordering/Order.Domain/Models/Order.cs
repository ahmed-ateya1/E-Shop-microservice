namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _items = new();
        public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

        public CustomerId CustomerId { get; private set; }
        public OrderName OrderName { get; private set; }

        public Address ShippingAddress { get; private set; }
        public Address BillingAddress { get; private set; }

        public Payment Payment { get; private set; }

        public OrderStatus? Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice => _items.Sum(x => x.Quantity * x.Price);
        private Order() { }

        public static Order Create(
            OrderId orderId,
            CustomerId customerId,
            OrderName OrderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment)
        {
            var order = new Order
            {
                Id = orderId,
                CustomerId = customerId,
                OrderName = OrderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            order.AddDomainEvent(new OrderCreatedDomainEvent(order));

            return order;
        }
        public void Update(
            Address shippingAddress,
            Address billingAddress,
            Payment payment,
            OrderStatus status)
        {
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdateDomainEvent(this));
        }
        public void AddOrderItem(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));

            var orderItem = new OrderItem(Id, productId, quantity, price);

            _items.Add(orderItem);

        }
        public void Remove(ProductId productId)
        {
            ArgumentNullException.ThrowIfNull(productId, nameof(productId));

            var orderItem = _items.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem == null)
            {
                throw new DomainException($"Order item with ProductId {productId} not found.");
            }
            _items.Remove(orderItem);
        }
    }
}