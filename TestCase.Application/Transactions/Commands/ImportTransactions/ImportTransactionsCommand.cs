using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestCase.Application.Transactions.Commands.ImportTransactions
{
    [DataContract]
    public class ImportTransactionsCommand : IRequest<string>
    {
        public IFormFile File { get; set; }
    }
}
