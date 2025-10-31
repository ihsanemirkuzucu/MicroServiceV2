using Asp.Versioning.Builder;
using MicroServiceV2.Discount.API.Features.Discounts.CreateDiscount;
using MicroServiceV2.Discount.API.Features.Discounts.GetDiscountByCode;

namespace MicroServiceV2.Discount.API.Features.Discounts
{
    public static class DiscountEndpointExtension
    {
        public static void AddDiscountGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts")
                .WithTags("Discounts")
                .WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItemEndpoint()
                .GetDiscountByCodeGroupItemEndpoint()
                .RequireAuthorization();

        }
    }
}
