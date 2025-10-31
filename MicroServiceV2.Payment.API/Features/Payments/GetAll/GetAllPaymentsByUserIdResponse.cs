namespace MicroServiceV2.Payment.API.Features.Payments.GetAll;

public record GetAllPaymentsByUserIdResponse(
    Guid Id,
    string OrderCode,
    string Amount,
    DateTime CreatedDate,
    PaymentStatus Status);