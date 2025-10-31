namespace MicroServiceV2.WEB.Pages.Order.ViewModel;

public record OrderItemViewModel(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice
);