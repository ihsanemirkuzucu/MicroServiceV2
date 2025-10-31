using MicroServiceV2.WEB.Pages.Basket.Dto;
using Refit;

namespace MicroServiceV2.WEB.Services.Refit
{
    public interface IDiscountRefitService
    {
        [Get("/api/v1/discounts/{coupon}")]
        Task<ApiResponse<GetDiscountByCouponResponse>> GetDiscountByCoupon(string coupon);
    }
}
