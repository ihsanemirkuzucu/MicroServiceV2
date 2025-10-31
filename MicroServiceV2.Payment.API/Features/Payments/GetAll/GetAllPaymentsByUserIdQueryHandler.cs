namespace MicroServiceV2.Payment.API.Features.Payments.GetAll
{
    public class GetAllPaymentsByUserIdQueryHandler(AppDbContext context,
        IIdentityService identity)
        : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(GetAllPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = identity.UserId;
            var payments = await context.Payments.Where(p => p.UserId == userId)
                .Select(x => new GetAllPaymentsByUserIdResponse(
                    x.Id,
                    x.OrderCode,
                    x.Amount.ToString("C"),
                    x.CreatedDate,
                    x.Status))
                .ToListAsync(cancellationToken);

            return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOk(payments);
        }
    }
}
