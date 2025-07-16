using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints
{
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/name/{name}", async (string name, ISender sender) =>
            {
                var query = new GetOrderByNameQuery(name);
                var orders = await sender.Send(query);
                return Results.Ok(orders);
            }).Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
              .WithDescription("Get orders by name")
              .WithName("GetOrdersByName")
              .WithTags("Orders");
        }
    }
}
