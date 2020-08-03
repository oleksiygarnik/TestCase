using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Application.Users.Queries;

namespace TestCase.Application.Auth.DTO
{
    public class AuthUserDto
    {
        public UserDto User { get; set; }

        public AccessTokenDto Token { get; set; }
    }
}
