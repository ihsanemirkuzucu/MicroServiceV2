namespace MicroServiceV2.Catalog.API.Features.Categories.Update
{
    public static class UpdateCategoryCommandEndpoint
    {
        public static RouteGroupBuilder UpdateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/",
                    async (UpdateCategoryCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToGenericResult())
                .WithName("UpdateCategory")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<UpdateCategoryCommand>>();
            return group;
        }
    }
}
