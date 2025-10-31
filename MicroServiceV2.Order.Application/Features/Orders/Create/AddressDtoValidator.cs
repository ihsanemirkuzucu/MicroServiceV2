using FluentValidation;
using MicroServiceV2.Order.Application.Dtos;

namespace MicroServiceV2.Order.Application.Features.Orders.Create;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Line)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.Province)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.District)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Matches(@"^\d{5}$").WithMessage("{PropertyName} must be 5 digits");
    }
}