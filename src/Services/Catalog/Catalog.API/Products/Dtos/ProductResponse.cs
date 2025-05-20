namespace Catalog.API.Products.Dtos
{
    public record ProductResponse(
        Guid Id,
        string Name,
        string Description,
        string ImageFile,
        decimal Price,
        List<string> Categories
    );
}
