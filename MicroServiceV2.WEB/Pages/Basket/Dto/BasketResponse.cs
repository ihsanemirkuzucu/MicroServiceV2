namespace MicroServiceV2.WEB.Pages.Basket.Dto;

public record BasketResponse(
    decimal? DiscountRate,
    string? Coupon,
    decimal TotalPrice,
    decimal? TotalPriceWithAppliedDiscount,
    List<BasketItemDto> Items
);