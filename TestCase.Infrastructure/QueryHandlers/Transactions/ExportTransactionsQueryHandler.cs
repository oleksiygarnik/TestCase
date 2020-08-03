using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Transactions.Queries;

namespace TestCase.Infrastructure.QueryHandlers.Transactions
{
    public class ExportTransactionsQueryHandler : IRequestHandler<ExportTransactionsQuery, XLWorkbook>
    {
        private readonly TransactionsContext _context;

        public ExportTransactionsQueryHandler(TransactionsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<XLWorkbook> Handle(ExportTransactionsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var transactions =  await _context.Transactions
                .FilterByType(request.Type)
                .FilterByStatus(request.Status)
                .ToListAsync();

            return transactions.BuildXLS();
        }
    }
}
