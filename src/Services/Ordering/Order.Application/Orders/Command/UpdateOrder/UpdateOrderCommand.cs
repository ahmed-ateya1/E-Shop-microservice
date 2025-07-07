using FluentValidation;

namespace Ordering.Application.Orders.Command.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResultResponse>;
    public record UpdateOrderResultResponse(Guid id);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
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
