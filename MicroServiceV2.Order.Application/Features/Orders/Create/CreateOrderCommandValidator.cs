using FluentValidation;

namespace MicroServiceV2.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.DiscountRate)
                .GreaterThan(0).When(x => x.DiscountRate.HasValue)
                .WithMessage("{PropertyName} must be a positive number or zero");

            //Bir propertysi olduğu için kontrol yapılmalıdır.
            RuleFor(x => x.Address)
                .NotNull().WithMessage("{PropertyName} is required")
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("{PropertyName} must contain at least one order item");

            //Bir propertysi olduğu için kontrol yapılmalıdır.
            RuleForEach(x => x.Items)
                .SetValidator(new OrderItemDtoValidator());

            //Bir propertysi olduğu için kontrol yapılmalıdır.
            RuleFor(x => x.Payment).NotNull().WithMessage("{PropertyName} is required")
                .SetValidator(new PaymentDtoValidator());
        }
    }
}
