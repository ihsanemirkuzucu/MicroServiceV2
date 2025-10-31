namespace MicroServiceV2.Catalog.API.Features.Categories.GetById;

public static class GetByIdCategoryEndpoint
{
    public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                    (await mediator.Send(new GetByIdCategoryQuery(id))).ToGenericResult())
            .WithName("GetByIdCategory")
            .MapToApiVersion(1, 0);
        return group;
    }
}