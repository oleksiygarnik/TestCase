using MediatR;
using System.Runtime.Serialization;

namespace TestCase.Application.Users.Queries
{
    [DataContract]
    public class UserByIdQuery : IRequest<UserDto>
    {
        [DataMember]
        public int Id { get; set; }
    }
}
