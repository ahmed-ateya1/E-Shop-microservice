using Ordering.Application.Orders.Command.CreateOrder;

namespace Ordering.API.Endpoints
{
    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid Id);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();

                var order = await sender.Send(command);
                
                var response = new CreateOrderResponse(order.orderId);


                return Results.Created($"/orders/{response.Id}",response.Id);
            }).Accepts<CreateOrderRequest>("application/json")
              .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
              .WithDescription("Create a new order")
              .WithName("CreateOrder")
              .WithTags("Orders");
        }
    }
}
