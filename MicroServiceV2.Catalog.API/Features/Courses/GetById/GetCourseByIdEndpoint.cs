namespace MicroServiceV2.Catalog.API.Features.Courses.GetById
{
    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                    async (IMediator mediator, Guid id) =>
                        (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult())
                .WithName("GetByIdCourse")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
