namespace MicroServiceV2.Basket.API.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponValidator: AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponValidator()
        {
            RuleFor(x => x.Coupon)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.DiscountRate).GreaterThan(0).WithMessage("{PropertyName} must be greather than 0");

        }
    }
}
