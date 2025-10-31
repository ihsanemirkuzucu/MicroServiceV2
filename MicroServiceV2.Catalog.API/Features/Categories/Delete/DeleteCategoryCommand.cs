namespace MicroServiceV2.Catalog.API.Features.Categories.Delete;

public record DeleteCategoryCommand(Guid Id) : IRequestByServiceResult;