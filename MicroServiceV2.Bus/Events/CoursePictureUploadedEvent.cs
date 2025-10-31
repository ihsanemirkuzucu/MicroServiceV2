namespace MicroServiceV2.Bus.Events;

public record CoursePictureUploadedEvent(Guid CourseId, string ImageUrl);