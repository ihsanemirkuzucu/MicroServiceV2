namespace MicroServiceV2.Catalog.API.Features.Categories.GetAll
{
    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult())
                .WithName("GelAllCategory")
                .MapToApiVersion(1, 0)
                .RequireAuthorization(policyNames: "ClientCredential");
            return group;
        }
    }
}
