using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestCase.Application.Users.Commands.DeleteUser
{
    [DataContract]
    public class DeleteUserCommand : IRequest
    {
        [DataMember]
        public int Id { get; set; }
    }
}
