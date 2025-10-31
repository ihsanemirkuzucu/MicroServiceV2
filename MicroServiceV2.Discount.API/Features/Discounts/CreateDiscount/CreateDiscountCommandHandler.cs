namespace MicroServiceV2.Discount.API.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context): IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasCodeForUser = await context.Discounts
                .AnyAsync(x => x.UserId.ToString() == request.UserId.ToString() && x.Code == request.Code, cancellationToken: cancellationToken);


            if (hasCodeForUser)
            {
                return ServiceResult.Error("Discount code already exists for this user", HttpStatusCode.BadRequest);
            }

            var newDiscount = new Discount
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Rate = request.Rate,
                UserId = request.UserId,
                CreatedDate = DateTime.Now,
                ExpireDate = request.ExpireDate
            };
            await context.Discounts.AddAsync(newDiscount,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
