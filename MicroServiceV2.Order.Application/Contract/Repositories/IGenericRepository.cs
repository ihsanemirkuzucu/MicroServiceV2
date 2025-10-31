using MicroServiceV2.Order.Domain.Entities;
using System.Linq.Expressions;

namespace MicroServiceV2.Order.Application.Contract.Repositories
{
    public interface IGenericRepository<TId, TEntity>
        where TId : struct
        where TEntity : BaseEntity<TId>

    {
        public Task<bool> AnyAsync(TId id);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllPageAsync(int pageNumber, int pageSize);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        //Memoryde daha az yer tutar
        ValueTask<TEntity?> GetByIdAsync(TId id);
    }
}
