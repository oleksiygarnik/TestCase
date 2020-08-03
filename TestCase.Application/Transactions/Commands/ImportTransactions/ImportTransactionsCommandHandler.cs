using CsvHelper;
using Domain;
using MediatR;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Transactions.Commands.ImportTransactions.CsvParser;
using TestCase.Application.Transactions.Queries;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Commands.ImportTransactions
{
    public class ImportTransactionsCommandHandler : IRequestHandler<ImportTransactionsCommand, Dictionary<string, List<int>>>
    {
        private readonly IUnitOfWork _work;
        private readonly IEntityRepository _repository;

        public ImportTransactionsCommandHandler(IUnitOfWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
            _repository = _work.EntityRepository;
        }
        public async Task<Dictionary<string, List<int>>> Handle(ImportTransactionsCommand request, CancellationToken cancellationToken)
        {
            if (request is null || request.File is null)
                throw new ArgumentNullException(nameof(request));

            // transactions ids for add or update operations
            var statesIds = new Dictionary<string, List<int>>()
            {
                ["Added"] = new List<int>(),
                ["Updated"] = new List<int>()
            };

            var csvParser = new TransactionsCsvParser(request.File);

            var dbTransactions = await _repository.GetAll<Transaction>();

            foreach (var transactionFromCsv in csvParser.Records)
            {
                if (dbTransactions.Contains(transactionFromCsv))
                {
                    dbTransactions.Single(t => t.Id == transactionFromCsv.Id).ChangeStatus(transactionFromCsv.Status);
                    statesIds["Updated"].Add(transactionFromCsv.Id);
                }
                else
                {
                    await _repository.Add(transactionFromCsv);
                    statesIds["Added"].Add(transactionFromCsv.Id);
                }
            }

            await _work.Commit(cancellationToken);

            return statesIds;
        }
    }
}
