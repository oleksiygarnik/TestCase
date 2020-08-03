using ClosedXML.Excel;
using MediatR;
using System.Runtime.Serialization;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Queries
{
    [DataContract]
    public class ExportTransactionsQuery : IRequest<XLWorkbook>
    {
        [DataMember]
        public TransactionStatus? Status { get; set; }

        [DataMember]
        public TransactionType? Type { get; set; }
    }
}
