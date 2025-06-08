namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string userName) : IQuery<BasketResponse>;
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, BasketResponse>
    {
        public async Task<BasketResponse> Handle(GetBasketQuery request, 
            CancellationToken cancellationToken)
        {
            return new BasketResponse();
        }
    }
}
