using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServiceV2.WEB.Dto;
using Refit;

namespace MicroServiceV2.WEB.Services.Refit
{
    public interface ICatalogRefitService
    {
        [Get("/api/v1.0/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetAllCategoriesAsync();

        [Get("/api/v1.0/courses/user/{userId}")]
        Task<ApiResponse<List<CourseDto>>> GetCourseByUserIdAsync(Guid userId);

        [Get("/api/v1/courses")]
        Task<ApiResponse<List<CourseDto>>> GetAllCourses();

        [Get("/api/v1/courses/{id}")]
        Task<ApiResponse<CourseDto>> GetCourse(Guid id);

        [Multipart]
        [Post("/api/v1.0/courses")]
        Task<ApiResponse<object>> CreateCourseAsync(
            [AliasAs("Name")] string Name,
            [AliasAs("Description")] string Description,
            [AliasAs("Price")] decimal Price,
            [AliasAs("Picture")] StreamPart? Picture,
            [AliasAs("CategoryId")] string CategoryId);

        [Put("/api/v1.0/courses")]
        Task<ApiResponse<object>> UpdateCourseAsync(UpdateCourseRequest updateCourseRequest);

        [Delete("/api/v1.0/courses/{id}")]
        Task<ApiResponse<object>> DeleteCourseAsync(Guid id);

    }
}
