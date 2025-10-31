using MicroServiceV2.Order.Application.Contract.Repositories;
using MicroServiceV2.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceV2.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context):
        GenericRepository<Guid,Domain.Entities.Order>(context),
        IOrderRepository
    {
        public async Task<List<Domain.Entities.Order>> GetOrdersByUserIdAsync(Guid customerId)
        {
            return await context.Orders
                .Include(x=>x.OrderItems)
                .Where(x => x.CustomerId == customerId).ToListAsync();
        }

        public async Task SetStatus(string orderCode,Guid paymentId, OrderStatus orderStatus)
        {
            var order = await context.Orders.FirstAsync(x => x.Code == orderCode);
            order.Status = orderStatus;
            order.PaymentId=paymentId;
            context.Update(order);
        }
    }
}
