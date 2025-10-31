namespace MicroServiceV2.Basket.API.Features.Baskets.Create
{
    public record CreateBasketItemCommand(Guid CourseId, string CourseName,decimal CoursePrice, string? ImageUrl): IRequestByServiceResult;
}
