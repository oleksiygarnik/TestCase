using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Infrastructure;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEntityRepository EntityRepository { get; }

        private readonly TransactionsContext _context;

        public UnitOfWork(TransactionsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            EntityRepository = new EntityRepository(context);
        }

        public async Task Commit(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}
