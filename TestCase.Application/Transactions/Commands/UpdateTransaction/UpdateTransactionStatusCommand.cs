using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TestCase.Application.Transactions.Queries;
using TestCase.Domain;

namespace TestCase.Application.Transactions.Commands.UpdateTransaction
{
    [DataContract]
    public class UpdateTransactionStatusCommand : IRequest<TransactionDto>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public TransactionStatus Status { get; set; }
    }
}
