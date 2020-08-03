using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TestCase.Application.Users.Queries;

namespace TestCase.Application.Users.Commands.UpdateUser
{
    [DataContract]
    public class UpdateUserCommand : IRequest<UserDto>
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
