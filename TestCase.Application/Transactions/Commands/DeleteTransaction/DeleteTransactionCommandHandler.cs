using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
    {
        private readonly IUnitOfWork _work;
        private readonly IEntityRepository _repository;

        public DeleteTransactionCommandHandler(IUnitOfWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
            _repository = work.EntityRepository;
        }

        public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var transactionForDelete = await _repository.SingleOrDefault<Transaction>(t => t.Id == request.Id, cancellationToken);

            if (transactionForDelete is null)
                throw new ArgumentNullException(nameof(transactionForDelete)); //return Unit.Value;

            await _repository.Remove(transactionForDelete);
            await _work.Commit();

            return Unit.Value;
        }
    }
}
