using Catalog.API.Products.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductEndpoint : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (ProductUpdateRequest request, ISender sender) =>
            {
                var result = await sender.Send(new UpdateProductCommand(request));
                return Results.Ok(result);
            })
                .WithName("UpdateProduct")
                .WithTags("Products")
                .Produces<ProductResponse>(StatusCodes.Status200OK)
                .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .WithDescription("Update a product")
                .WithDisplayName("Update a product");

        }
    }
}
