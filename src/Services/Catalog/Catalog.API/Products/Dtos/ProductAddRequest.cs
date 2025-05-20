namespace Catalog.API.Products.Dtos
{
    public record ProductAddRequest(
        string Name,
        string Description,
        string ImageFile,
        decimal Price,
        List<string> Categories
    );
}
