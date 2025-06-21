using Basket.API.Models;

namespace Basket.API.Dtos
{
    public class BasketResponse
    {
        public string UserName { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; set; } 
        public decimal TotalPrice { get; set; }
    }
}
