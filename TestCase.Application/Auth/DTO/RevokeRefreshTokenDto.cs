using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Auth.DTO
{
    public sealed class RevokeRefreshTokenDto
    {
        public string RefreshToken { get; set; }
    }
}
