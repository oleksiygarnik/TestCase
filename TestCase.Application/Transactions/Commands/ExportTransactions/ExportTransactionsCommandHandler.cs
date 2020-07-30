using ClosedXML.Excel;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Transactions.Queries;
using TestCase.Domain;
using System.Linq;

namespace TestCase.Application.Transactions.Commands.ExportTransactions
{
    public class ExportTransactionsCommandHandler : IRequestHandler<ExportTransactionsCommand, XLWorkbook>
    {
        private readonly IUnitOfWork _work;
        private readonly IEntityRepository _repository;

        public ExportTransactionsCommandHandler(IUnitOfWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
            _repository = _work.EntityRepository;
        }
        public async Task<XLWorkbook> Handle(ExportTransactionsCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var workbook = new XLWorkbook();

            var transactions = await _repository.GetAll<Transaction>();

            transactions = transactions.ToList();

            IXLWorksheet worksheet = workbook.Worksheets.Add("Transactions");
            

            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Status";
            worksheet.Cell(1, 3).Value = "Type";
            worksheet.Cell(1, 4).Value = "ClientName";
            worksheet.Cell(1, 5).Value = "Amount";
            for (int index = 1; index <= transactions.Count(); index++)
            {
                worksheet.Cell(index + 1, 1).Value = transactions.ToList()[index - 1].Id;
                worksheet.Cell(index + 1, 2).Value = transactions.ToList()[index - 1].Status;
                worksheet.Cell(index + 1, 3).Value = transactions.ToList()[index - 1].Type;
                worksheet.Cell(index + 1, 3).Value = transactions.ToList()[index - 1].ClientName;
                worksheet.Cell(index + 1, 3).Value = transactions.ToList()[index - 1].Amount;
            }

            return workbook;
        }
    }
}
