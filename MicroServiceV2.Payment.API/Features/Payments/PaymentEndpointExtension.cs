using Asp.Versioning.Builder;
using MicroServiceV2.Payment.API.Features.Payments.Create;
using MicroServiceV2.Payment.API.Features.Payments.GetAll;
using MicroServiceV2.Payment.API.Features.Payments.GetStatus;

namespace MicroServiceV2.Payment.API.Features.Payments
{
    public static class PaymentEndpointExtension
    {
        public static void AddPaymentGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments")
                .WithTags("Payments")
                .WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint()
                .GetAllPaymentsByUserPaymentGroupItemEndpoint()
                .GetPaymentStatusGroupItemEndpoint();

        }
    }
}
