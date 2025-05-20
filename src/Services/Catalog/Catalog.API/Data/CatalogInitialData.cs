using Catalog.API.Models;
using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetProducts());

            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = new Guid("456EC544-1EEA-4348-B52A-5892F7FB8D5C"),
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 10.99m,
                    Categories = new List<string> { "Category1", "Category2" },
                    ImageFile = "image1.jpg",
                },
                new Product
                {
                    Id = new Guid("BF46BE77-0FE6-4413-90BA-6911DBC466CF"),
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = 20.99m,
                    Categories = new List<string> { "Category2", "Category3" },
                    ImageFile = "image2.jpg",
                }

            };
        }
    }
}
