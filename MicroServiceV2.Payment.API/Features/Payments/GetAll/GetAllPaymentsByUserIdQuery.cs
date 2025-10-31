namespace MicroServiceV2.Payment.API.Features.Payments.GetAll;

public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;