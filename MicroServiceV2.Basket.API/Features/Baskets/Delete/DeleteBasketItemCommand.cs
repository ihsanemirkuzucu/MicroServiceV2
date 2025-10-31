namespace MicroServiceV2.Basket.API.Features.Baskets.Delete;

public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;

