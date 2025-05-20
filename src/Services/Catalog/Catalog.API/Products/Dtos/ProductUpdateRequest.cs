namespace Catalog.API.Products.Dtos
{
    public record ProductUpdateRequest(
        Guid Id,
        string Name,
        string Description,
        string ImageFile,
        decimal Price,
        List<string> Categories
    );
}
