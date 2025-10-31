namespace MicroServiceV2.Order.Application.Dtos;

public record OrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice);