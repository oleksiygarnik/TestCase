using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Auth.Exceptions
{
    public sealed class InvalidTokenException : Exception
    {
        public InvalidTokenException(string tokenName) : base($"Invalid {tokenName} token.") { }
    }
}
