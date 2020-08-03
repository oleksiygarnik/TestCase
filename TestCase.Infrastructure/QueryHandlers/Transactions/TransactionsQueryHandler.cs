using Application;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Transactions.Queries;
using TestCase.Domain;
using TestCase.Infrastructure.QueryHandlers.Extensions;

namespace TestCase.Infrastructure.QueryHandlers.Transactions
{
    public class TransactionsQueryHandler : IRequestHandler<TransactionsQuery, PagedResponse<TransactionDto>>
    {
        private readonly TransactionsContext _context;

        public TransactionsQueryHandler(TransactionsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<PagedResponse<TransactionDto>> Handle(TransactionsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var transactions = await _context.Transactions
                .FilterByType(request.Type)
                .FilterByStatus(request.Status)
                .Select(t => new TransactionDto()
                {
                    Id = t.Id,
                    Status = t.Status,
                    Type = t.Type,
                    Amount = t.Amount,
                    ClientName = t.ClientName
                })
                .Paginate(request, cancellationToken);

            if (transactions == null)
                throw new ArgumentNullException(nameof(transactions));

            var options = new PageOptions(transactions.Current, transactions.Size);

            return new PagedResponse<TransactionDto>(options, transactions.Total, transactions.Items.ToArray());

        }
    }
}
