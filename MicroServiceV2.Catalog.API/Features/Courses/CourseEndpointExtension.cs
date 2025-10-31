using Asp.Versioning.Builder;
using MicroServiceV2.Catalog.API.Features.Courses.Create;
using MicroServiceV2.Catalog.API.Features.Courses.Delete;
using MicroServiceV2.Catalog.API.Features.Courses.GetAll;
using MicroServiceV2.Catalog.API.Features.Courses.GetAllByUserId;
using MicroServiceV2.Catalog.API.Features.Courses.GetById;
using MicroServiceV2.Catalog.API.Features.Courses.Update;

namespace MicroServiceV2.Catalog.API.Features.Courses
{
    public static class CourseEndpointExtension
    {
        public static void AddCourseGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses")
                .WithTags("Courses")
                .WithApiVersionSet(apiVersionSet)
                .CreateCourseGroupItemEndpoint()
                .GetAllCourseGroupItemEndpoint()
                .GetByIdCourseGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCourseGroupItemEndpoint()
                .GetByUserIdCourseGroupItemEndpoint();
        }
    }
}
