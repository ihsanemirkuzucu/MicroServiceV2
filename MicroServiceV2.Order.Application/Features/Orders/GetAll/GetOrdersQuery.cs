using MicroServiceV2.Shared;

namespace MicroServiceV2.Order.Application.Features.Orders.GetAll;

public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersQueryResponse>>;

