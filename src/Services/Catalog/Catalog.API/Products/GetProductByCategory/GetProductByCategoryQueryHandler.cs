using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.Dtos;
using Marten;
using Mapster;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<IEnumerable<ProductResponse>>;

    public class GetProductByCategoryQueryHandler(IDocumentSession _session)
        : IQueryHandler<GetProductByCategoryQuery, IEnumerable<ProductResponse>>
    {
        public async Task<IEnumerable<ProductResponse>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _session.Query<Product>()
                .Where(x => x.Categories.Any(c => c.Equals(request.category, StringComparison.OrdinalIgnoreCase)))
                .ToListAsync(cancellationToken);

            var response = products.Adapt<IEnumerable<ProductResponse>>();

            return response;
        }
    }
}
