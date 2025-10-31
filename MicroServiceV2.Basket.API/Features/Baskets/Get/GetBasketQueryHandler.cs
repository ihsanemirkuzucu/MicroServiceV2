namespace MicroServiceV2.Basket.API.Features.Baskets.Get
{
    public class GetBasketQueryHandler(IMapper mapper, BasketService basketService)
        :IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
            var basketDto = mapper.Map<BasketDto>(basket);
            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
