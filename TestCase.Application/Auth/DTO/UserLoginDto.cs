using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Auth.DTO
{
    public sealed class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
