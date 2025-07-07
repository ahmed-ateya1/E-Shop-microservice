using FluentValidation;

namespace Ordering.Application.Orders.Command.DeleteOrder
{
    public record DeleteOrderCommand(Guid orderId):ICommand<bool>;

    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.orderId).NotEmpty().WithMessage("Order Id cannot be empty.");
        }
    }

}
