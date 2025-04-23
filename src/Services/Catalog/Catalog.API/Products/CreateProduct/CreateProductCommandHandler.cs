using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name , string Description , string ImageFile , decimal Price , List<string> Categories)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
                Categories = request.Categories,
                Id = Guid.NewGuid()
            };

            //save to DB

            //return result 

            return new CreateProductResult(product.Id);
        }
    }
}
