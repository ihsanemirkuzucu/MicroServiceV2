namespace MicroServiceV2.Order.Application.Contract.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken=default);
        Task CommitWithTransactionAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
