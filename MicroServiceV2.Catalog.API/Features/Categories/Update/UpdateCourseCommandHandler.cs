namespace MicroServiceV2.Catalog.API.Features.Categories.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context)
        : IRequestHandler<UpdateCategoryCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);
           if(hasCategory == null)
            {
                return ServiceResult.ErrorAsNotFount();
            }
            hasCategory.Name = request.Name;
            context.Categories.Update(hasCategory);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
