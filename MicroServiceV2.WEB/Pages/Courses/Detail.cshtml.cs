using MicroServiceV2.WEB.PageModels;
using MicroServiceV2.WEB.Services;
using MicroServiceV2.WEB.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServiceV2.WEB.Pages.Courses
{
    [AllowAnonymous]
    public class DetailModel(CatalogService catalogService) : BasePageModel
    {
        public CourseViewModel? Course { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var courseAsResult = await catalogService.GetCourse(id);

            if (courseAsResult.IsFail) return ErrorPage(courseAsResult);

            Course = courseAsResult.Data!;
            return Page();
        }
    }
}
