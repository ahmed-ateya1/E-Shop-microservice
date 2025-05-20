using Catalog.API.Products.Dtos;
using System.Net;

namespace Catalog.API.Products.GetProductById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("product/{Id:guid}", async (ISender sender, Guid Id) =>
            {
                var response = await sender.Send(new GetProductByIdQuery(Id));

                if (response is null)
                {
                    return Results.NotFound(new ApiResponse<ProductResponse>
                    {
                        IsSuccess = false,
                        Message = "Product Not Found",
                        StatusCode = HttpStatusCode.NotFound
                    });
                }

                return Results.Ok(new ApiResponse<ProductResponse>
                {
                    IsSuccess = true,
                    Message = "Product Found Seccussfully",
                    StatusCode = HttpStatusCode.OK,
                    Result = response
                });
            })
            .WithDescription("Get Product By Id")
            .WithDisplayName("getProductById")
            .WithSummary("Get Product By Id")
            .Produces<ApiResponse<ProductResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest, "application/problem+json")
            .ProducesProblem(StatusCodes.Status404NotFound, "application/problem+json")
            .ProducesProblem(StatusCodes.Status500InternalServerError, "application/problem+json");
        }
    }
}
