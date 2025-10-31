#region

using System.Collections.Immutable;
using MicroServiceV2.WEB.Pages.Order.ViewModel;

#endregion

namespace MicroServiceV2.WEB.Pages.Order.ViewModel;

public record OrderHistoryViewModel(string DateTime, string TotalPrice)
{
    private List<OrderItemViewModel> OrderItems { get; } = [];

    public ImmutableList<OrderItemViewModel> GetItems => OrderItems.ToImmutableList();


    public void AddItem(Guid productId, string productName, decimal unitPrice)
    {
        OrderItems.Add(new OrderItemViewModel(productId, productName, unitPrice));
    }
}