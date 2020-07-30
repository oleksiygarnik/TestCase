using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestCase.Application.Transactions.Commands.DeleteTransaction
{
    [DataContract]
    public class DeleteTransactionCommand : IRequest
    {
        [DataMember]
        public int Id { get; set; }
    }
}
