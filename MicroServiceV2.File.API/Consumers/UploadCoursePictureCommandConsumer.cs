﻿using MicroServiceV2.Bus.Commands;
using MicroServiceV2.Bus.Events;

namespace MicroServiceV2.File.API.Consumers
{
    public class UploadCoursePictureCommandConsumer(IServiceProvider serviceProvider) : IConsumer<UploadCoursePictureCommand>
    {
        public async Task Consume(ConsumeContext<UploadCoursePictureCommand> context)
        {
            using var scope = serviceProvider.CreateScope();
            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();
            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(context.Message.FileName)}";
            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);
            await System.IO.File.WriteAllBytesAsync(uploadPath, context.Message.Picture);

            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            await publishEndpoint.Publish(new CoursePictureUploadedEvent(context.Message.CourseId, $"files/{newFileName}"));
        }
    }
}
