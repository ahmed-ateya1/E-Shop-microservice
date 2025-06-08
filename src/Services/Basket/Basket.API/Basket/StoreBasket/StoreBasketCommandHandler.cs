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
        public async Task<BasketResponse> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            return new BasketResponse();
        }
    }
}
