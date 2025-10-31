using FluentValidation;
using MicroServiceV2.Order.Application.Dtos;

namespace MicroServiceV2.Order.Application.Features.Orders.Create;

public class PaymentDtoValidator : AbstractValidator<PaymentDto>
{
    public PaymentDtoValidator()
    {
        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.CardHolderName)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.Cvc)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.Expiration)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
    }
}