namespace MicroServiceV2.WEB.Pages.Order.Dto;

public record CreateOrderRequest(
    decimal? DiscountRate,
    AddressDto Address,
    PaymentDto Payment,
    List<OrderItemDto> Items);