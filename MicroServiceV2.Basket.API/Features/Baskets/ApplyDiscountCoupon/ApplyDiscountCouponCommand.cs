namespace MicroServiceV2.Basket.API.Features.Baskets.ApplyDiscountCoupon;

public record ApplyDiscountCouponCommand(string Coupon, decimal DiscountRate) : IRequestByServiceResult;

