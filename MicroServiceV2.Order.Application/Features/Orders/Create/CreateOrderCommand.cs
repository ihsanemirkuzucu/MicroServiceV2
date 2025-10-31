using MicroServiceV2.Order.Application.Dtos;
using MicroServiceV2.Shared;

namespace MicroServiceV2.Order.Application.Features.Orders.Create
{
    public record CreateOrderCommand(
        decimal? DiscountRate,
        AddressDto Address,
        PaymentDto Payment,
        List<OrderItemDto> Items) : IRequestByServiceResult;
}