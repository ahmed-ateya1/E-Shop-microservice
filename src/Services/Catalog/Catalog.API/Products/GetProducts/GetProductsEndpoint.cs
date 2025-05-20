using Catalog.API.Products.Dtos;
using System.Net;

namespace Catalog.API.Products.GetProducts
{
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products",async([AsParameters]GetProductRequest request,ISender sender) =>
            {
                var response = await sender.Send(new GetProductsQuery(request));

                return Results.Ok(new ApiResponse<IEnumerable<ProductResponse>>
                {
                    IsSuccess = true,
                    Message = "Product Created",
                    StatusCode = HttpStatusCode.OK,
                    Result = response
                });
            })
             .WithName("GetProducts")
             .Produces<ApiResponse<IEnumerable<ProductResponse>>>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest, "application/problem+json")
             .WithSummary("Get Products")
             .WithDescription("Get Products");
        }
    }
}
