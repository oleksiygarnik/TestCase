using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Auth.Exceptions
{
    public sealed class InvalidUsernameOrPasswordException : Exception
    {
        public InvalidUsernameOrPasswordException() : base("Invalid username or password.") { }
    }
}
