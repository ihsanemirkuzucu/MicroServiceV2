namespace MicroServiceV2.Discount.API.Features.Discounts.GetDiscountByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext context) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {
            var hasDiscount = await context.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken: cancellationToken);
            if (hasDiscount == null)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount code not found", HttpStatusCode.NotFound);
            }

            if (hasDiscount.ExpireDate < DateTime.Now)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount code has expired", HttpStatusCode.BadRequest);
            }

            return ServiceResult<GetDiscountByCodeQueryResponse>.SuccessAsOk(
                new GetDiscountByCodeQueryResponse(hasDiscount.Code, hasDiscount.Rate));
        }
    }
}
