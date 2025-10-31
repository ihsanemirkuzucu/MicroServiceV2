using MicroServiceV2.Bus.Events;

namespace MicroServiceV2.Discount.API.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var newDiscount = new Features.Discounts.Discount
            {
                Id = NewId.NextSequentialGuid(),
                Code = DiscountCodeGenerator.Generate(),
                Rate = 0.5M,
                UserId = context.Message.UserId,
                CreatedDate = DateTime.Now,
                ExpireDate =DateTime.Now.AddMonths(1)
            };
            await appDbContext.Discounts.AddAsync(newDiscount);
            await appDbContext.SaveChangesAsync();
        }
    }
}
