using Asp.Versioning.Builder;
using MicroServiceV2.File.API.Features.File.Delete;
using MicroServiceV2.File.API.Features.File.Upload;

namespace MicroServiceV2.File.API.Features.File
{
    public static class FileEndpointExtension
    {
        public static void AddFileGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files")
                .WithTags("Files")
                .WithApiVersionSet(apiVersionSet)
                .UploadFileGroupItemEndpoint()
                .DeleteFileGroupItemEndpoint()
                .RequireAuthorization();

        }
    }
}
