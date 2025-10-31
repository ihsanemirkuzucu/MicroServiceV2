namespace MicroServiceV2.Discount.API.Features.Discounts.CreateDiscount
{
    public record CreateDiscountCommand(string Code, decimal Rate, Guid UserId, DateTime ExpireDate)
        : IRequestByServiceResult;
}
