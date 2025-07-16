using Ordering.Application.Orders.Command.DeleteOrder;

namespace Ordering.API.Endpoints
{
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteOrderCommand(id);

                var result = await sender.Send(command);
                if (result)
                {
                    return Results.NoContent();
                }
                return Results.NotFound();
            }).Produces(StatusCodes.Status204NoContent)
              .Produces(StatusCodes.Status404NotFound)
              .WithDescription("Delete an existing order")
              .WithName("DeleteOrder")
              .WithTags("Orders");
        }
    }
}
