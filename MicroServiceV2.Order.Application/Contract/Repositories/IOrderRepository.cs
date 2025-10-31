using MicroServiceV2.Order.Domain.Entities;

namespace MicroServiceV2.Order.Application.Contract.Repositories
{
    public interface IOrderRepository : IGenericRepository<Guid, Domain.Entities.Order>
    {
        public Task<List<Domain.Entities.Order>> GetOrdersByUserIdAsync(Guid customerId);

        public Task SetStatus(string orderCode, Guid paymentId, OrderStatus orderStatus);
    }
}

