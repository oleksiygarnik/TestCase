using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCase.Application.Transactions.Commands.ImportTransactions
{
    public class ImportTransactionsCommandHandler : IRequestHandler<ImportTransactionsCommand, string>
    {
        private readonly IUnitOfWork _work;

        public ImportTransactionsCommandHandler(IUnitOfWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
        }
        public async Task<string> Handle(ImportTransactionsCommand request, CancellationToken cancellationToken)
        {
            var pathToSave = Directory.GetCurrentDirectory();

            var fileName = ContentDispositionHeaderValue.Parse(request.File.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                if(request.File.Length > 0)
                    await request.File.CopyToAsync(stream);
            }

            return "lol";
        }
    }
}
