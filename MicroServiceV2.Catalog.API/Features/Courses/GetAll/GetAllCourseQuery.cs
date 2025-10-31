namespace MicroServiceV2.Catalog.API.Features.Courses.GetAll;

public record GetAllCourseQuery() : IRequestByServiceResult<List<CourseDto>>;