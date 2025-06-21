using Basket.API.Data;
using Basket.API.Models;
using Mapster;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(BasketAddRequest request) : ICommand<BasketResponse>;

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.request.UserName).NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.request.Items).NotEmpty().WithMessage("At least one item is required in the basket.");
        }
    }
    public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, BasketResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public StoreBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<BasketResponse> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = command.request.Adapt<ShoppingCart>();

            var result = await _basketRepository.StoreBasketAsync(basket, cancellationToken);

            return result.Adapt<BasketResponse>();
        }
    }
}
