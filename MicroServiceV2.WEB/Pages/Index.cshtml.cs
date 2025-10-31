using MicroServiceV2.WEB.PageModels;
using MicroServiceV2.WEB.Services;
using MicroServiceV2.WEB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServiceV2.WEB.Pages
{
    public class IndexModel(CatalogService catalogService, ILogger<IndexModel> logger) : BasePageModel
    {
        public List<CourseViewModel>? Courses { get; set; } = [];


        public async Task<IActionResult> OnGet()
        {
            var coursesAsResult = await catalogService.GetAllCoursesAsync();

            if (coursesAsResult.IsFail) return ErrorPage(coursesAsResult);

            Courses = coursesAsResult.Data!;

            return Page();
        }
    }
}
