using Asp.Versioning.Builder;
using MicroServiceV2.Catalog.API.Features.Categories.Create;
using MicroServiceV2.Catalog.API.Features.Categories.Delete;
using MicroServiceV2.Catalog.API.Features.Categories.GetAll;
using MicroServiceV2.Catalog.API.Features.Categories.GetById;
using MicroServiceV2.Catalog.API.Features.Categories.Update;

namespace MicroServiceV2.Catalog.API.Features.Categories
{
    public static class CategoryEndpointExtension
    {
        public static void AddCategoryGroupEndpointExtension(this WebApplication app,ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories")
                .WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint()
                .UpdateCategoryGroupItemEndpoint()
                .DeleteCategoryGroupItemEndpoint();
        }
    }
}
