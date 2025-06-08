using Basket.API.Models;

namespace Basket.API.Dtos
{
    public class BasketAddRequest
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
