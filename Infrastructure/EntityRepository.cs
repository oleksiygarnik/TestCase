using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class EntityRepository : IEntityRepository
    {
        private readonly DbContext _context;
        public EntityRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Add(entity);

            return Task.CompletedTask;
        }

        public async Task Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
                await Add(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>()
            where TEntity : class
            => await _context.Set<TEntity>().ToListAsync();

        public Task Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Remove(entity);

            return Task.CompletedTask;
        }

        public async Task Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
                await Remove(entity);
        }

        // with cancellationToken

        public Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {
            return _context.Set<TEntity>()
                .Where(predicate)
                .CountAsync(cancellationToken);

        }

        public async Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {
            return await _context.Set<TEntity>()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {

            return await _context.Set<TEntity>()
                .Where(predicate)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {

            return await _context.Set<TEntity>()
                .Where(predicate)
                .SingleOrDefaultAsync(cancellationToken);
        }

        // without cancellationToken
        public Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Find(predicate, CancellationToken.None);
        }

        public Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Count(predicate, CancellationToken.None);
        }

        public Task<TEntity> SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return SingleOrDefault(predicate, CancellationToken.None);
        }

        public Task<TEntity> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return FirstOrDefault(predicate, CancellationToken.None);
        }
    }
}
