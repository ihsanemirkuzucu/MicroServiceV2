namespace MicroServiceV2.Basket.API.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(BasketService basketService): IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;
            if (!basket.Items.Any())
            {
                return ServiceResult<BasketDto>.Error("Basket Item not found", HttpStatusCode.NotFound);
            }
            // Apply the new discount to the basket
            basket.ApplyNewDiscount(request.Coupon,request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(basket);
            await basketService.CreateBasketCacheAsync(basket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();


        }
    }
}
