using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.API.Endpoints
{
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/orders/customer/{customerId:guid}", async (Guid customerId, ISender sender) =>
            {
                var query = new GetOrderByCustomerQuery(customerId);
                var orders = await sender.Send(query);
                return Results.Ok(orders);
            }).Produces<List<OrderDto>>(StatusCodes.Status200OK)
              .WithDescription("Get orders by customer ID")
              .WithName("GetOrdersByCustomer")
              .WithTags("Orders");
        }
    }
}
