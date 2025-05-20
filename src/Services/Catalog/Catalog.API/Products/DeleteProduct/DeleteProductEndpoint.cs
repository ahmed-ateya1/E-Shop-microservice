using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndpoint : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                return Results.Ok(result);
            })
                .WithName("DeleteProduct")
                .WithTags("Products")
                .Produces<bool>(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .WithDescription("Delete a product")
                .WithDisplayName("Delete a product");
        }
    }
}
