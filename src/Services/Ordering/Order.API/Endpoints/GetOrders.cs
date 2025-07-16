using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrder;

namespace Ordering.API.Endpoints
{
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters]PaginationRequest paginationRequest, ISender sender) =>
            {
                var query = new GetOrderQuery(paginationRequest);
                var orders = await sender.Send(query);
                return Results.Ok(orders);
            }).Produces<List<OrderDto>>(StatusCodes.Status200OK)
              .WithDescription("Get all orders")
              .WithName("GetOrders")
              .WithTags("Orders");
        }
    }
}
