namespace MicroServiceV2.Catalog.API.Features.Categories.Delete;

public class DeleteCategoryCommandHandler(AppDbContext context)
    : IRequestHandler<DeleteCategoryCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);
        if (hasCategory == null)
        {
            return ServiceResult.ErrorAsNotFount();
        }

        context.Remove(hasCategory);
        await context.SaveChangesAsync(cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}