using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.Dtos;
using System.Runtime.CompilerServices;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(ProductUpdateRequest productUpdate):IQuery<ProductResponse>;
    public class ProductUpdateRequestValidator : AbstractValidator<UpdateProductCommand>
    {
        public ProductUpdateRequestValidator()
        {

            RuleFor(p => p.productUpdate.Name)
                .NotEmpty()
                .WithMessage("Product name is required");
            RuleFor(p => p.productUpdate.Description)
                .NotEmpty()
                .WithMessage("Product description is required");
            RuleFor(p => p.productUpdate.ImageFile)
                .NotEmpty()
                .WithMessage("Product image file is required");
            RuleFor(p => p.productUpdate.Price)
                .GreaterThan(0)
                .WithMessage("Product price must be greater than 0");
        }
    }

    public class UpdateProductCommandHandler (IDocumentSession _session)
        : IQueryHandler<UpdateProductCommand, ProductResponse>
    {
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _session.LoadAsync<Product>(request.productUpdate.Id, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(request.productUpdate.Id);

            product = request.productUpdate.Adapt(product);

            _session.Store(product);
            await _session.SaveChangesAsync(cancellationToken);

            return product.Adapt<ProductResponse>();

        }
    }
}
