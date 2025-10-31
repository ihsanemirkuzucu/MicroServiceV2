using MicroServiceV2.Order.Application.Dtos;

namespace MicroServiceV2.Order.Application.Features.Orders.GetAll;

public record GetOrdersQueryResponse(DateTime CreatedDate, decimal TotalPrice, List<OrderItemDto> OrderItems);
