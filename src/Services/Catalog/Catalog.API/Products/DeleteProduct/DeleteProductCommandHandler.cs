using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IQuery<bool>;
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required.");
        }
    }
    public class DeleteProductCommandHandler(IDocumentSession _session) : IQueryHandler<DeleteProductCommand, bool>
    {
        
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product is null)
                throw new ProductNotFoundException(request.Id);
            _session.Delete(product);
            await _session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
