namespace MicroServiceV2.Catalog.API.Features.Courses.Delete;

public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;