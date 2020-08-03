using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEntityRepository
    {
        Task <IEnumerable<TEntity>> GetAll<TEntity>()
            where TEntity : class;

        Task Add<TEntity>(TEntity entity)
            where TEntity : class;

        Task Add<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        Task Remove<TEntity>(TEntity entity)
            where TEntity : class;

        Task Remove<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        // with cancellationToken
        Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate,
           CancellationToken cancellationToken)
           where TEntity : class;

        Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate,
           CancellationToken cancellationToken)
           where TEntity : class;

        Task<TEntity> SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate,
          CancellationToken cancellationToken)
          where TEntity : class;

        Task<TEntity> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate,
          CancellationToken cancellationToken)
          where TEntity : class;

        // without cancellationToken

        Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : class;

        Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        Task<TEntity> SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        Task<TEntity> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

    }
}
