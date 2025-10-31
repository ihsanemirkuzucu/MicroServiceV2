namespace MicroServiceV2.Catalog.API.Features.Courses.GetAllByUserId;

public record GetCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;