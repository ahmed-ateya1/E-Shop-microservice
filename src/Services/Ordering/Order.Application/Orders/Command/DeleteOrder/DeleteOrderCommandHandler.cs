using BuildingBlocks.Exceptions;

namespace Ordering.Application.Orders.Command.DeleteOrder
{
    public class DeleteOrderCommandHandler:ICommandHandler<DeleteOrderCommand, bool>
    {
        private readonly IApplicationDbContext _db;
        public DeleteOrderCommandHandler(IApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.orderId);
            var order = await _db.Orders.FindAsync([orderId], cancellationToken);
            if (order == null)
            {
                throw new NotFoundException($"Order Not Found with Id {command.orderId}");
            }
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
