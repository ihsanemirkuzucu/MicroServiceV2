namespace MicroServiceV2.Order.Application.Contract.Refit.PaymentService;

public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);

