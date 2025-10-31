using FluentValidation;
using MicroServiceV2.Order.Application.Dtos;

namespace MicroServiceV2.Order.Application.Features.Orders.Create;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
    }
}