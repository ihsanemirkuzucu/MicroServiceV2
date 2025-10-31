namespace MicroServiceV2.Basket.API.Dtos
{
    public record BasketItemDto(
        Guid Id,
        string Name,
        string? ImageUrl,
        decimal Price,
        decimal? PriceByApplyDiscountRate);
}
