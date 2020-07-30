using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestCase.Application.Users.Queries
{
    [DataContract]
    public class UserByIdQuery : IRequest<UserDto>
    {
        [DataMember]
        public int Id { get; set; }
    }
}
