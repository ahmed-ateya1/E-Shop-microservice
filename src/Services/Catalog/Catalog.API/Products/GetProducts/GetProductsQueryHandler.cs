using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.Dtos;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductRequest(int? pageIndex = 1 , int? pageSize = 10);
    public record GetProductsQuery : IQuery<IEnumerable<ProductResponse>>
    {
        public GetProductRequest Request { get; init; } 

        public GetProductsQuery(GetProductRequest request)
        {
            Request = request;
        }
    }

    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<ProductResponse>>
    {
        private readonly IDocumentSession _session;

        public GetProductsQueryHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _session.Query<Product>()
                .ToPagedListAsync(request.Request.pageIndex ?? 1, request.Request.pageSize ?? 10);

            var response = products.Adapt<IEnumerable<ProductResponse>>();

            return response;
        }
    }

}
