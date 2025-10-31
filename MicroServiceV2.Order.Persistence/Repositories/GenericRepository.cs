﻿using MicroServiceV2.Order.Application.Contract.Repositories;
using MicroServiceV2.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MicroServiceV2.Order.Persistence.Repositories
{
    public class GenericRepository<TId, TEntity>(AppDbContext context) : IGenericRepository<TId, TEntity>
        where TId : struct
        where TEntity : BaseEntity<TId>

    {
        protected AppDbContext Context = context;
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id.Equals(id));


        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) => _dbSet.AnyAsync(predicate);

        public Task<List<TEntity>> GetAllAsync() => _dbSet.ToListAsync();


        public Task<List<TEntity>> GetAllPageAsync(int pageNumber, int pageSize) =>
            _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();


        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate);


        public void Add(TEntity entity) => _dbSet.Add(entity);


        public void Update(TEntity entity) => _dbSet.Update(entity);


        public void Delete(TEntity entity) => _dbSet.Remove(entity);


        public ValueTask<TEntity?> GetByIdAsync(TId id) => _dbSet.FindAsync(id);

    }
}
