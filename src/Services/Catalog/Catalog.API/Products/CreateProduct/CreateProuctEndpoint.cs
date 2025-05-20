using Catalog.API.Products.Dtos;
using System.Net;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, string ImageFile, decimal Price, List<string> Categories);
    public record CreateProductResponse(Guid Id);
    public class CreateProuctEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (ProductAddRequest request, IMediator mediator) =>
            {
                var command = new CreateProductCommand(request);
                var result = await mediator.Send(command);
                return Results.Ok(result);
            })
             .WithName("CreateProduct")
             .Produces<ApiResponse<ProductResponse>>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest, "application/problem+json")
             .WithSummary("Create Product")
             .WithDescription("Create Product");

        }
    }
}
