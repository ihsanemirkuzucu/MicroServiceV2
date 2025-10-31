using MicroServiceV2.WEB.Pages.Auth.SignIn;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServiceV2.WEB.Pages.Auth
{
    public class SignInModel(SignInService signInService) : PageModel
    {
        [BindProperty] public required SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExampleModel;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await signInService.AuthenticateAsync(SignInViewModel);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Fail!.Title!);
                ModelState.AddModelError(string.Empty, result.Fail.Detail!);

                return Page();
            }
            return RedirectToPage("/Index");
        }

        //                      asp-page-handler= SignOut yazarak çaðýrýlýr.
        public async Task<IActionResult> OnGetSignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
