namespace MicroServiceV2.Payment.API.Features.Payments.GetStatus
{
    public class GetPaymentStatusHandler(AppDbContext appDbContext) : IRequestHandler<GetPaymentStatusRequest, ServiceResult<GetPaymentStatusResponse>>
    {
        public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusRequest request, CancellationToken cancellationToken)
        {
            var payment = await appDbContext.Payments.FirstOrDefaultAsync(x => x.OrderCode == request.OrderCode,cancellationToken);
            if (payment is null)
                return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(null, false));

            return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(payment.Id, payment.Status == PaymentStatus.Success));
        }
    }
}
