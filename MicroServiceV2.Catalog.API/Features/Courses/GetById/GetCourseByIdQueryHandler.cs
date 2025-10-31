namespace MicroServiceV2.Catalog.API.Features.Courses.GetById;

public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
{
    public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var hasCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (hasCourse == null)
        {
            return ServiceResult<CourseDto>
                .Error("Course not found.",
                    $"The course with id ({request.Id}) was not found",
                    HttpStatusCode.NotFound);
        }

        var category = await context.Categories.FindAsync(hasCourse.CategoryId, cancellationToken);
        hasCourse.Category=category!;
        var courseAsDto = mapper.Map<CourseDto>(hasCourse);
        return ServiceResult<CourseDto>.SuccessAsOk(courseAsDto);
    }
}