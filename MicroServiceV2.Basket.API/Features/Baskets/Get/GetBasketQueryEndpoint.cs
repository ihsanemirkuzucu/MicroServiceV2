namespace MicroServiceV2.Basket.API.Features.Baskets.Get
{
    public static class GetBasketQueryEndpoint
    {
        public static RouteGroupBuilder GetBasketItemGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetBasketQuery())).ToGenericResult())
                .WithName("GetBasketItem")
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}
