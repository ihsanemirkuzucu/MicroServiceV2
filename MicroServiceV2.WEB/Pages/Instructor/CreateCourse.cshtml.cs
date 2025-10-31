using MicroServiceV2.WEB.Services;
using MicroServiceV2.WEB.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServiceV2.WEB.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        [BindProperty] public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;

        public async Task OnGetAsync()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();

            if(categoriesResult.IsFail)
            {
                //redirect to error page
            }
            ViewModel.SetCategoryDropdownList(categoriesResult.Data!);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var createCourseResult = await catalogService.CreateCourseAsync(ViewModel);
            if (!createCourseResult.IsSuccess)
            {
                //redirect to error page
            }
            return RedirectToPage("Courses");
        }
    }
}
