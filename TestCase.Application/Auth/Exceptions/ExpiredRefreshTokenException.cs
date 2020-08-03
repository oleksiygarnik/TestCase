using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Auth.Exceptions
{
    public sealed class ExpiredRefreshTokenException : Exception
    {
        public ExpiredRefreshTokenException() : base("Refresh token expired.") { }
    }
}
