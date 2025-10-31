using MediatR;
using MicroServiceV2.Order.Application.Features.Orders.Create;
using MicroServiceV2.Shared.Extensions;
using MicroServiceV2.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceV2.Order.API.Endpoints.Orders
{
    public static class CreateOrderEndpoint
    {
        public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateOrder")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();
            return group;
        }
    }
}
