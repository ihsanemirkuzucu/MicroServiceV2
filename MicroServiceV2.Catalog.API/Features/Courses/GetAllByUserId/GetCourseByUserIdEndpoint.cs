namespace MicroServiceV2.Catalog.API.Features.Courses.GetAllByUserId
{
    public static class GetCourseByUserIdEndpoint
    {
        public static RouteGroupBuilder GetByUserIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}",
                    async (IMediator mediator, Guid userId) =>
                        (await mediator.Send(new GetCourseByUserIdQuery(userId))).ToGenericResult())
                .WithName("GetByUserIdCourse")
                .MapToApiVersion(1, 0)
                .RequireAuthorization(policyNames: "Instructor");
            return group;
        }
    }
}
