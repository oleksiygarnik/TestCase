using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Auth.DTO
{
    public sealed class RefreshTokenDTO
    {
        public RefreshTokenDTO()
        {
            SigningKey = Environment.GetEnvironmentVariable("SecretJWTKey");
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        [JsonIgnore]
        public string SigningKey { get; private set; }
    }
}
