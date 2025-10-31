namespace MicroServiceV2.Catalog.API.Features.Courses.GetAll
{
    public static class GetAllCourseEndpoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllCourseQuery())).ToGenericResult())
                .WithName("GetAllCourse")
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}
