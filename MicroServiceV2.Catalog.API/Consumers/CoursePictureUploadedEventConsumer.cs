using MicroServiceV2.Bus.Events;

namespace MicroServiceV2.Catalog.API.Consumers
{
    public class CoursePictureUploadedEventConsumer(IServiceProvider serviceProvider) : IConsumer<CoursePictureUploadedEvent>
    {
        public async Task Consume(ConsumeContext<CoursePictureUploadedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext= scope.ServiceProvider.GetService<AppDbContext>();
            var course = await dbContext.Courses.FindAsync(context.Message.CourseId);
            if (course == null)
            {
                throw new NullReferenceException("course not found");
            }
            course.ImageUrl = context.Message.ImageUrl;
            await dbContext.SaveChangesAsync();
        }
    }
}
