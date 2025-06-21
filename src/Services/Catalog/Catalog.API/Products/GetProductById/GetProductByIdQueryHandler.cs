using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.Dtos;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid ID) : IQuery<ProductResponse>;
    internal class GetProductByIdQueryHandler(IDocumentSession _session) : IQueryHandler<GetProductByIdQuery, ProductResponse>
    {
        public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(request.ID,cancellationToken);

            if (product is null)
                return null;

            return product.Adapt<ProductResponse>();
        }
    }
}
