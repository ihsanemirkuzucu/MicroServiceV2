namespace MicroServiceV2.Payment.API.Features.Payments.Create
{
    public class CreateCommandValidator:AbstractValidator<CreatePaymentCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.OrderCode).NotEmpty().WithMessage("Order code is required.");
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Card number is required.");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Card holder name is required.");
            RuleFor(x => x.CardExpirationDate).NotEmpty().WithMessage("Card expiration date is required.");
            RuleFor(x => x.CardSecurityNumber).NotEmpty().WithMessage("Card security number is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
