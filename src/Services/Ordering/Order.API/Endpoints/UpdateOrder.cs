using Ordering.Application.Orders.Command.UpdateOrder;

namespace Ordering.API.Endpoints
{
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(Guid Id);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var order = await sender.Send(command);
                if (order == null)
                {
                    return Results.NotFound();
                }
                var response = new UpdateOrderResponse(order.id);
                return Results.Ok(response);
            }).Accepts<UpdateOrderRequest>("application/json")
              .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .WithDescription("Update an existing order")
              .WithName("UpdateOrder")
              .WithTags("Orders");
        }
    }
}
