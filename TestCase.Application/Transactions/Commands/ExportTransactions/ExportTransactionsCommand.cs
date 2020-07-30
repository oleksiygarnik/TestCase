using ClosedXML.Excel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Commands.ExportTransactions
{
    [DataContract]
    public class ExportTransactionsCommand : IRequest<XLWorkbook>
    {
        [DataMember]
        public TransactionStatus? Status { get; set; }

        [DataMember]
        public TransactionType? Type { get; set; }
    }
}
