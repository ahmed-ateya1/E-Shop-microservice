namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, string ImageFile, decimal Price, List<string> Categories);
    public record CreateProductResponse(Guid Id);
    public class CreateProuctEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Ok(response);
            })
             .WithName("CreateProduct")
             .Produces<CreateProductResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest, "application/problem+json")
             .WithSummary("Create Product")
             .WithDescription("Create Product");

        }
    }
}
