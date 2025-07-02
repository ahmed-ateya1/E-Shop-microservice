namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        private Product(ProductId productId, string name, decimal price)
        {
            Id = productId;
            Name = name;
            Price = price;
        }
        private Product() { } 
        public static Product Create(ProductId productId, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            return new Product(productId, name, price);
        }
    }
}
