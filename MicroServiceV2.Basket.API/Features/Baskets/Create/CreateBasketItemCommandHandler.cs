namespace MicroServiceV2.Basket.API.Features.Baskets.Create
{
    public class CreateBasketItemCommandHandler(IIdentityService identity, BasketService basketService)
        : IRequestHandler<CreateBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCacheAsync(cancellationToken);

            Data.Basket? currentBasket;
            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl,
                request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                currentBasket = new Data.Basket(identity.UserId, [newBasketItem]);
                await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
            var existBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);
            if (existBasketItem is not null)
            {
                currentBasket.Items.Remove(existBasketItem);
            }
            currentBasket.Items.Add(newBasketItem);
            currentBasket.ApplyAvailableDiscount();// Apply any existing discounts to the new basket item

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
