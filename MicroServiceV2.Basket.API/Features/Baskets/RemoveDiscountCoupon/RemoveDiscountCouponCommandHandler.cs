namespace MicroServiceV2.Basket.API.Features.Baskets.RemoveDiscountCoupon;

public class RemoveDiscountCouponCommandHandler(BasketService basketService)
    : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCacheAsync(cancellationToken);
        if (string.IsNullOrEmpty(basketAsJson))
        {
            return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
        }
        var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;
        basket.RemoveDiscount();

        basketAsJson = JsonSerializer.Serialize(basket);
        await basketService.CreateBasketCacheAsync(basket, cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}