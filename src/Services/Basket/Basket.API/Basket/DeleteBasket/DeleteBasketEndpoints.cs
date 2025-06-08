
namespace Basket.API.Basket.DeleteBasket
{
    public class DeleteBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteBasketCommand(userName));
                if (result)
                {
                    return Results.NoContent();
                }
                return Results.NotFound($"Basket for user '{userName}' not found.");
            })
            .WithDescription("Delete a basket for a user")
            .WithName("DeleteBasket")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithTags("Basket");
        }
    }
}
