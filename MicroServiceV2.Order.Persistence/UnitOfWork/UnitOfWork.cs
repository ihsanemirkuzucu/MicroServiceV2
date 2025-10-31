using MicroServiceV2.Order.Application.Contract.UnitOfWork;

namespace MicroServiceV2.Order.Persistence.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public async Task CommitWithTransactionAsync(CancellationToken cancellationToken)
            => await context.Database.CommitTransactionAsync(cancellationToken);

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
            => await context.Database.BeginTransactionAsync(cancellationToken);
    }
}
