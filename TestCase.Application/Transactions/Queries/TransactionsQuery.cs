using Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Queries
{
    [DataContract]
    public class TransactionsQuery : PagedRequest<TransactionDto>
    {
        [DataMember]
        public TransactionStatus? Status { get; set; }

        [DataMember]
        public TransactionType? Type { get; set; }
    }
}
