#region

using MicroServiceV2.WEB.Pages.Order.ViewModel;

#endregion

namespace MicroServiceV2.WEB.Pages.Order.Dto;

public record GetOrderHistoryResponse(DateTime Created, decimal TotalPrice, List<OrderItemViewModel> Items);