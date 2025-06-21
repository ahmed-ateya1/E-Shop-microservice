namespace Basket.API.Basket.GetBasket
{
    public class GetBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                if(result == null)
                {
                    return Results.NotFound($"Basket for this userName {userName} Not found");
                }

                return Results.Ok(result);
            }).WithDescription("Get a user's basket")
                .WithName("GetBasket")
                .Produces<BasketResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError)
                .WithTags("BasketEndpoints");
        }
    }
}
