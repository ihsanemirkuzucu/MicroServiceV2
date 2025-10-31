namespace MicroServiceV2.Catalog.API.Features.Categories.Update
{
    public record UpdateCategoryCommand(Guid Id, string Name) : IRequestByServiceResult;
}
