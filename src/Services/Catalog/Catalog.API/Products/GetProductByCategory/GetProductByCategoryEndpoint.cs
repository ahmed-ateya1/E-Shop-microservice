
using Catalog.API.Products.Dtos;

namespace Catalog.API.Products.GetProductByCategory
{
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                return Results.Ok(result);
            })
              .WithName("GetProductByCategory")
              .Produces<ApiResponse<IEnumerable<ProductResponse>>>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest, "application/problem+json")
              .WithSummary("Get Product By Category")
              .WithDescription("Get Product By Category");
        }
    }
}
