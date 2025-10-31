namespace MicroServiceV2.Catalog.API.Features.Categories.GetById;
//veritabanında state değişmediği için query olarak adlandırıyoruz
public record GetByIdCategoryQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
