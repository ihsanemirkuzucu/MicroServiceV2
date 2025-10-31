using MicroServiceV2.Bus.Commands;
using MicroServiceV2.Shared.Services;

namespace MicroServiceV2.Catalog.API.Features.Courses.Create
{
    public class CreateCourseCommandHandler
        (AppDbContext context,
            IMapper mapper,
            IIdentityService identityService,
            IPublishEndpoint publishEndpoint)
        : IRequestHandler<CreateCourseCommand, ServiceResult<CreateCourseResponse>>
    {
        public async Task<ServiceResult<CreateCourseResponse>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories
                .AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!hasCategory)
            {
                return ServiceResult<CreateCourseResponse>
                    .Error("Category not found.", $"Category with id {request.CategoryId} does not exist.", HttpStatusCode.NotFound);
            }

            var hasCourse = await context.Courses
                .AnyAsync(c => c.Name == request.Name, cancellationToken);
            if (hasCourse)
            {
                return ServiceResult<CreateCourseResponse>
                    .Error("Course already exists.", $"Course with name {request.Name} already exists.", HttpStatusCode.BadRequest);
            }

            var newCourse = mapper.Map<Course>(request);
            newCourse.CreatedDate = DateTime.Now;
            newCourse.UserId = identityService.UserId;
            newCourse.Feature = new Feature
            {
                Duration = 10,
                EducatorFullName = "Ahmet Yılmaz",
                Rating = 0,
            };
            newCourse.Id = NewId.NextSequentialGuid();
            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            if (request.Picture is not null)
            {
                using var memoryStream = new MemoryStream();
                await request.Picture.CopyToAsync(memoryStream, cancellationToken);
                var pictureAsByteArray = memoryStream.ToArray();
                var uploadCoursePictureCommand = new UploadCoursePictureCommand(newCourse.Id, pictureAsByteArray, request.Picture.FileName);
                await publishEndpoint.Publish(uploadCoursePictureCommand, cancellationToken);
            }

            return ServiceResult<CreateCourseResponse>.SuccessAsCreated(new CreateCourseResponse(newCourse.Id), $"/api/courses/{newCourse.Id}");

        }
    }
}
