using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Products.Dtos;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(ProductAddRequest productAddRequest)
        : ICommand<ProductResponse>;

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.productAddRequest.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.productAddRequest.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.productAddRequest.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
            RuleFor(x => x.productAddRequest.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.productAddRequest.Categories).NotEmpty().WithMessage("At least one category is required.");
        }
    }

    public class CreateProductCommandHandler(IDocumentSession _session)
        : ICommandHandler<CreateProductCommand, ProductResponse>
    {


        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var product = request.productAddRequest.Adapt<Product>();
            product.Id = Guid.NewGuid();
            _session.Store(product);
            await _session.SaveChangesAsync(cancellationToken);

            return product.Adapt<ProductResponse>();
        }
    }
}
