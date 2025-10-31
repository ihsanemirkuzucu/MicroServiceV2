namespace MicroServiceV2.Catalog.API.Features.Categories.GetById;

public class GetByIdCategoryCommandHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdCategoryQuery, ServiceResult<CategoryDto>>
{
    public async Task<ServiceResult<CategoryDto>> Handle(GetByIdCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var hasCategory = await context.Categories.FindAsync( request.Id, cancellationToken);
        if (hasCategory is null)
        {
            return ServiceResult<CategoryDto>.Error("Category not found.",$"The category with Id({request.Id}) not found.",HttpStatusCode.NotFound);
        }

        var categoryAsDto = mapper.Map<CategoryDto>(hasCategory);
        return ServiceResult<CategoryDto>.SuccessAsOk(categoryAsDto);
    }
}