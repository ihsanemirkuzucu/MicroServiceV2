using MicroServiceV2.WEB.PageModels;
using MicroServiceV2.WEB.Pages.Order.ViewModel;
using MicroServiceV2.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroServiceV2.WEB.Pages.Order
{
    [Authorize]
    public class HistoryModel(OrderService orderService) : BasePageModel
    {
        public List<OrderHistoryViewModel> OrderHistoryList { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var response = await orderService.GetHistory();


            if (response.IsFail) return ErrorPage(response);

            OrderHistoryList = response.Data!;


            return Page();
        }
    }
}
