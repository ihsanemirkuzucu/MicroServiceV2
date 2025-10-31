using AutoMapper;
using MediatR;
using MicroServiceV2.Order.Application.Contract.Repositories;
using MicroServiceV2.Order.Application.Dtos;
using MicroServiceV2.Shared;
using MicroServiceV2.Shared.Services;

namespace MicroServiceV2.Order.Application.Features.Orders.GetAll
{
    public class GetOrdersQueryHandler(IIdentityService identity,
        IOrderRepository orderRepository,
        IMapper mapper)
        :IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersQueryResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersByUserIdAsync(identity.UserId);
            var response = orders.Select(o =>
                new GetOrdersQueryResponse(o.CreatedDate, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();
            return ServiceResult<List<GetOrdersQueryResponse>>.SuccessAsOk(response);
        }
    }
}
