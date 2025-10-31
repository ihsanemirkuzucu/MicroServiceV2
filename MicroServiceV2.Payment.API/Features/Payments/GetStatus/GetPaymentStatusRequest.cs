namespace MicroServiceV2.Payment.API.Features.Payments.GetStatus;

public record GetPaymentStatusRequest(string OrderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;