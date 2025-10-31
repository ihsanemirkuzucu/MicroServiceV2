using Microsoft.AspNetCore.Mvc;

namespace MicroServiceV2.Basket.API.Features.Baskets.Create
{
    public static class CreateBasketItemCommandEndpoint
    {
        public static RouteGroupBuilder CreateBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item",
                    async (CreateBasketItemCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateBasketItem")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateBasketItemCommand>>();
            return group;
        }
    }
}
