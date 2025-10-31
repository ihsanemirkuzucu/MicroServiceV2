namespace MicroServiceV2.Basket.API.Features.Baskets.Create
{
    public class CreateBasketItemValidator:AbstractValidator<CreateBasketItemCommand>
    {
        public CreateBasketItemValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().NotNull().WithMessage("{propertyName} is required");

            RuleFor(x => x.CourseName)
                .NotEmpty().NotNull().WithMessage("{propertyName} is required");

            RuleFor(x=>x.CoursePrice)
                .GreaterThan(0).WithMessage("{propertyName} must be greater than zero");

        }
    }
}
