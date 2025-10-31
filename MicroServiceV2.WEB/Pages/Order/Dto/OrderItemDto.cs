namespace MicroServiceV2.WEB.Pages.Order.Dto;

public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice
);