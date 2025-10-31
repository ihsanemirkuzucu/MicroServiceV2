using MicroServiceV2.WEB.Services;
using MicroServiceV2.WEB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServiceV2.WEB.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public List<CourseViewModel> CourseViewModels { get; set; }
        public async Task OnGetAsync()
        {
            var result = await catalogService.GetCoursesByUserId();

            if (result.IsFail)
            {
                //redirect to error page
            }
            CourseViewModels = result.Data!;
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var result = await catalogService.DeleteAsync(id);
            if (result.IsFail)
            {
                //   redirect to error page
            }
            return RedirectToPage();
        }
    }
}
