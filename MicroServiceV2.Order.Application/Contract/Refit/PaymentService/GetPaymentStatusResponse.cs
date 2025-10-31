namespace MicroServiceV2.Order.Application.Contract.Refit.PaymentService;

public record GetPaymentStatusResponse(Guid? PaymentId, bool IsPaid);