using MicroServiceV2.Shared.Filters;

namespace MicroServiceV2.Payment.API.Features.Payments.Create
{
    public static class CreatePaymentEndpoint
    {
        public static RouteGroupBuilder CreatePaymentGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async ([FromBody] CreatePaymentCommand command, [FromServices] IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult())
                .WithName("CreatePayment")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status204NoContent)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreatePaymentCommand>>()
                .RequireAuthorization("Password");
            return group;
        }
    }
}
