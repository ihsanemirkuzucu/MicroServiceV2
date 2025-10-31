using MediatR;
using MicroServiceV2.Order.Application.Features.Orders.GetAll;
using MicroServiceV2.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceV2.Order.API.Endpoints.Orders
{
    public static class GetOrdersEndpoint
    {
        public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                    (await mediator.Send(new GetOrdersQuery())).ToGenericResult())
                .WithName("GetOrders")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
            return group;
        }
    }
}
