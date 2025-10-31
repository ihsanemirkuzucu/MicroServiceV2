namespace MicroServiceV2.Catalog.API.Features.Courses.GetAllByUserId;

public class GetCourseByUserIdQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>>
{
    public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request, CancellationToken cancellationToken)
    {
        var courses = await context.Courses
            .Where(x=>x.UserId==request.Id)
            .ToListAsync(cancellationToken: cancellationToken);
        var categories = await context.Categories.ToListAsync(cancellationToken);
        foreach (var course in courses)
        {
            course.Category = categories.First(x => x.Id == course.CategoryId);
        }

        var coursesAsDto = mapper.Map<List<CourseDto>>(courses);
        return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesAsDto);
    }
}