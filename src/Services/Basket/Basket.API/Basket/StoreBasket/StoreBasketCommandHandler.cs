using Basket.API.Data;
using Basket.API.Models;
using Discount.GRPC;
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
        private readonly DiscountProtoService.DiscountProtoServiceClient _client;

        public StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient client)
        {
            _basketRepository = basketRepository;
            _client = client;
        }

        public async Task<BasketResponse> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
         
            var basket = command.request.Adapt<ShoppingCart>();

            await DeductDiscount(basket);

            var result = await _basketRepository.StoreBasketAsync(basket, cancellationToken);

            return result.Adapt<BasketResponse>();
        }

        private async Task DeductDiscount(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await _client.GetDiscountAsync(
                    new GetDiscountRequest
                    {
                        ProductName = item.ProductName
                    });

                if (coupon != null && !string.IsNullOrEmpty(coupon.ProductName))
                {
                    var discountAmount = (decimal)coupon.Amount;

                    item.UnitPrice = Math.Max(item.UnitPrice - discountAmount, 0);
                }
            }
        }

    }
}
