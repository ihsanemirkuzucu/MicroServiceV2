namespace MicroServiceV2.Payment.API.Features.Payments.GetAll
{
    public static class GetAllPaymentsByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentsByUserPaymentGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllPaymentsByUserIdQuery())).ToGenericResult())
                .WithName("GetAllPaymentsByUserPayment")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .RequireAuthorization("ClientCredential");

            return group;
        }
    }
}
