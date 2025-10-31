namespace MicroServiceV2.Order.Application.Contract.Refit.PaymentService;

public record CreatePaymentRequest(
    string OrderCode,
    string CardNumber,
    string CardHolderName,
    string CardExpirationDate,
    string CardSecurityNumber,
    decimal Amount);
