
namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (BasketAddRequest request , IMediator mediator) =>
            {
                var result = await mediator.Send(new StoreBasketCommand(request));

                return Results.Created($"/basket/{result.UserName}",result);
            }).WithDescription("Store a basket for a user")
              .WithName("StoreBasket")
              .Produces<BasketResponse>(StatusCodes.Status201Created)
              .Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status500InternalServerError)
              .WithTags("Basket");
        }
    }
}
