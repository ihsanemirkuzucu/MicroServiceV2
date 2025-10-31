namespace MicroServiceV2.Basket.API.Features.Baskets.Delete
{
    public static class DeleteBasketItemCommandEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:guid}",
                    async (Guid id, IMediator mediator) =>
                        (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult())
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}
