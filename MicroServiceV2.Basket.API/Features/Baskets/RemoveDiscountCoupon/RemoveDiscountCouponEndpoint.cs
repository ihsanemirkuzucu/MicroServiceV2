namespace MicroServiceV2.Basket.API.Features.Baskets.RemoveDiscountCoupon
{
    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder DeleteDiscountCouponItemGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon",
                    async (IMediator mediator) =>
                        (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult())
                .WithName("DeleteDiscountCoupon")
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}
