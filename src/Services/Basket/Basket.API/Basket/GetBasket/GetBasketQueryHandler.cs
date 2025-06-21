using Basket.API.Data;
using Mapster;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string userName) : IQuery<BasketResponse>;
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, BasketResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<BasketResponse> Handle(GetBasketQuery request, 
            CancellationToken cancellationToken)
        {
            var result = await _basketRepository.GetBasketAsync(request.userName, cancellationToken);
            if (result == null)
                return null;

            return result.Adapt<BasketResponse>();

        }
    }
}
