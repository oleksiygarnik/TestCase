using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TestCase.Application.Transactions.Queries;

namespace TestCase.Application.Transactions.Commands.ImportTransactions
{
    [DataContract]
    public class ImportTransactionsCommand : IRequest<Dictionary<string, List<int>>>
    {
        public IFormFile File { get; set; }
    }
}
