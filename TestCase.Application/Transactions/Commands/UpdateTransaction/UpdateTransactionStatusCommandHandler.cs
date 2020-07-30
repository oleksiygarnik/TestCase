using Domain;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Transactions.Queries;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionStatusCommandHandler : IRequestHandler<UpdateTransactionStatusCommand, TransactionDto>
    {
        private readonly IUnitOfWork _work;
        private readonly IEntityRepository _repository;

        public UpdateTransactionStatusCommandHandler(IUnitOfWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
            _repository = _work.EntityRepository;
        }
        public async Task<TransactionDto> Handle(UpdateTransactionStatusCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var transaction = await _repository.SingleOrDefault<Transaction>(t => t.Id == request.Id, cancellationToken);

            if (transaction is null)
                throw new NotFoundEntityException(nameof(transaction));

            transaction.ChangeStatus(request.Status);

            await _work.Commit();

            return new TransactionDto()
            {
                Id = transaction.Id,
                Status = transaction.Status,
                Type = transaction.Type,
                ClientName = transaction.ClientName
            };

            //AutoMapper
        }
    }
}
