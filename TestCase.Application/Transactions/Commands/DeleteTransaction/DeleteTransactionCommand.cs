using MediatR;
using System.Runtime.Serialization;

namespace TestCase.Application.Transactions.Commands.DeleteTransaction
{
    [DataContract]
    public class DeleteTransactionCommand : IRequest
    {
        [DataMember]
        public int Id { get; set; }
    }
}
