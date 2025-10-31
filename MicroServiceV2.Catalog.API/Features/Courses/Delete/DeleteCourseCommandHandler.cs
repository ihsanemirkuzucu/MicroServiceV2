namespace MicroServiceV2.Catalog.API.Features.Courses.Delete;

public class DeleteCourseCommandHandler(AppDbContext context)
    : IRequestHandler<DeleteCourseCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var hascourse = await context.Courses.FindAsync(request.Id, cancellationToken);
        if (hascourse == null)
        {
            return ServiceResult.ErrorAsNotFount();
        }

        context.Remove(hascourse);
        await context.SaveChangesAsync(cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}