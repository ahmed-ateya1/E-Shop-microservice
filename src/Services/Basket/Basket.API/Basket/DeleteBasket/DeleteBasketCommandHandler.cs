
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName): ICommand<bool>;

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
    public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, bool>
    {
        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
