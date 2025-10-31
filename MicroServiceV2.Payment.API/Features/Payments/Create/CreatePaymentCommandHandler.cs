namespace MicroServiceV2.Payment.API.Features.Payments.Create
{
    public class CreatePaymentCommandHandler(AppDbContext context,
        IIdentityService identity)
    : IRequestHandler<CreatePaymentCommand, ServiceResult<CreatePaymentResponse>>
    {
        public async Task<ServiceResult<CreatePaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var userId = identity.UserId;
            var userName = identity.Username;
            var roles = identity.Roles;

            var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(
                request.CardNumber,
                request.CardHolderName,
                request.CardExpirationDate,
                request.CardSecurityNumber,
                request.Amount);
            if (!isSuccess)
            {
                return ServiceResult<CreatePaymentResponse>.Error("Payment Fail",$"{errorMessage}",HttpStatusCode.BadRequest);
            }
            var newPayment = new Repositories.Payment(identity.UserId, request.OrderCode, request.Amount);
            newPayment.SetStatus(PaymentStatus.Success);
            context.Payments.Add(newPayment);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult<CreatePaymentResponse>.SuccessAsOk(new CreatePaymentResponse(newPayment.Id,true,null));
        }



        private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber, string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
        {
            await Task.Delay(1000);
            return (true, null);
            //return (false, "Payment processing failed due to insufficient funds.");
        }
    }
}
