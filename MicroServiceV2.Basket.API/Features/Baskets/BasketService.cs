using MicroServiceV2.Basket.API.Consts;
using Microsoft.Extensions.Caching.Distributed;

namespace MicroServiceV2.Basket.API.Features.Baskets
{
    public class BasketService(IIdentityService identity, IDistributedCache cache)
    {
        private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identity.UserId);
        private string GetCacheKey(Guid userId)  => string.Format(BasketConst.BasketCacheKey, userId);

        public async Task<string?> GetBasketFromCacheAsync(CancellationToken cancellationToken)
        {
            return await cache.GetStringAsync(GetCacheKey(), cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await cache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
        }

        public async Task DeleteBasket(Guid userId)
        {
            await cache.RemoveAsync(GetCacheKey(userId));
        }
    }
}
