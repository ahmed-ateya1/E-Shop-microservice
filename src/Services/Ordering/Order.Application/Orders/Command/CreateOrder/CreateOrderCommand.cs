using FluentValidation;

namespace Ordering.Application.Orders.Command.CreateOrder
{
    public record CreateOrderCommand (OrderDto Order) : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid orderId);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order).NotNull();
            RuleFor(x => x.Order.CustomerId).NotEmpty();
            RuleFor(x => x.Order.OrderName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Order.ShippingAddress).NotNull();
            RuleFor(x => x.Order.BillingAddress).NotNull();
            RuleFor(x => x.Order.Payment).NotNull();
        }
    }
}
