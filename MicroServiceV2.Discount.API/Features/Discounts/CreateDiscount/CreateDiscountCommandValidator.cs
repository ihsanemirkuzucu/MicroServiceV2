namespace MicroServiceV2.Discount.API.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandValidator:AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().NotNull().WithMessage("Code is required.")
                .Length(10).WithMessage("{propertyName} must be 10 characters long");
            RuleFor(x => x.Rate)
                .NotEmpty().NotNull().WithMessage("Rate is required.");
            RuleFor(x => x.UserId)
                .NotEmpty().NotNull().WithMessage("UserId is required.");
            RuleFor(x => x.ExpireDate)
                .NotEmpty().NotNull().WithMessage("ExpireDate is required.");
        }
    }

}
