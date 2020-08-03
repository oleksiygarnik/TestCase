using System;
using System.Text.Json.Serialization;

namespace TestCase.Application.Auth.DTO
{
    public sealed class RefreshTokenDto
    {
        public RefreshTokenDto()
        {
            SigningKey = Environment.GetEnvironmentVariable("SecretJWTKey");
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        [JsonIgnore]
        public string SigningKey { get; private set; }
    }
}
