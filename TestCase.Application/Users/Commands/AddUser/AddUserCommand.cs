using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TestCase.Application.Users.Queries;

namespace TestCase.Application.Users.Commands.AddUser
{
    [DataContract]
    public class AddUserCommand : IRequest<UserDto>
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
