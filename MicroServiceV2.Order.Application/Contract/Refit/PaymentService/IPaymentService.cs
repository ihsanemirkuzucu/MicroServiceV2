using Refit;

namespace MicroServiceV2.Order.Application.Contract.Refit.PaymentService
{
    public interface IPaymentService
    {
        [Post("/api/v1/payments")]
        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest createPaymentRequest);

        [Get("/api/v1/payments/status/{orderCode}")]
        Task<GetPaymentStatusResponse> GetStatusAsync(string orderCode);
    }
}
