using Asp.Versioning.Builder;
using MicroServiceV2.Basket.API.Features.Baskets.ApplyDiscountCoupon;
using MicroServiceV2.Basket.API.Features.Baskets.Create;
using MicroServiceV2.Basket.API.Features.Baskets.Delete;
using MicroServiceV2.Basket.API.Features.Baskets.Get;
using MicroServiceV2.Basket.API.Features.Baskets.RemoveDiscountCoupon;

namespace MicroServiceV2.Basket.API.Features.Baskets
{
    public static class BasketEndpointExtension
    {
        public static void AddBasketGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets")
                .WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .CreateBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupEndpoint()
                .GetBasketItemGroupEndpoint()
                .ApplyDiscountCouponItemGroupItemEndpoint()
                .DeleteDiscountCouponItemGroupEndpoint()
                .RequireAuthorization("Password");
        }
    }
}
