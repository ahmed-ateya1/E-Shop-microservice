using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(
        IBasketRepository basketRepository,
        IDistributedCache cache
        ) : IBasketRepository
    {
       
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if(!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

            var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);

            if (basket == null)
                return null;

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;

        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            var basket = await basketRepository.StoreBasketAsync(shoppingCart, cancellationToken);

            if (basket == null)
                return null;

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
        {
            var result =  await basketRepository.DeleteBasketAsync(userName, cancellationToken);

            if (result)
            {
                await cache.RemoveAsync(userName, cancellationToken);
            }
            return result;
        }

    }
}
