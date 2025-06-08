namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public Guid ProductID { get; set; }
    }
}
