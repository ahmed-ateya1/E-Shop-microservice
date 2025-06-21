using Basket.API.Exceptions;
using Basket.API.Models;
using Marten;

namespace Basket.API.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ILogger<BasketRepository> _logger;
        private readonly IDocumentSession _session;

        public BasketRepository(ILogger<BasketRepository> logger, IDocumentSession session)
        {
            _logger = logger;
            _session = session;
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
        {
            var basket = await _session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            if (basket == null)
            {
                _logger.LogWarning("Basket not found for user: {UserName}", userName);
                return false;
            }
            _session.Delete(basket);

            await _session.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await _session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            if (basket == null)
            {
                _logger.LogWarning("Basket not found for user: {UserName}", userName);
            }

            return basket is null ? throw new BasketNotFoundException($"User Not Found") : basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            _session.Store(shoppingCart);
            await _session.SaveChangesAsync(cancellationToken);

            return shoppingCart;
        }
    }
}
