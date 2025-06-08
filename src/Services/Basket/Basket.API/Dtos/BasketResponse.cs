namespace Basket.API.Dtos
{
    public class BasketResponse
    {
        public string UserName { get; set; } = string.Empty;
        public List<BasketItemResponse> Items { get; set; } = new List<BasketItemResponse>();
        public decimal TotalPrice { get; set; }
    }
}
