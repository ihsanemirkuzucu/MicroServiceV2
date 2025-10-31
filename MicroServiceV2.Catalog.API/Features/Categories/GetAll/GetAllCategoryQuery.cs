namespace MicroServiceV2.Catalog.API.Features.Categories.GetAll;
//veritabanında state değişmediği için query olarak adlandırıyoruz
public record GetAllCategoryQuery:IRequestByServiceResult<List<CategoryDto>>;