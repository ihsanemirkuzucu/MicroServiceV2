namespace MicroServiceV2.Catalog.API.Features.Categories.Create;

//veritbanında state değiştiği için command olarak adlandırıyoruz
public record CreateCategoryCommand(string Name) : IRequestByServiceResult<CreateCategoryResponse>;

