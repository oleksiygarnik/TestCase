using System;

namespace TestCase.Application.Auth.Common
{
    public sealed class AccessToken
    {
        public string Token { get; }
        public int ExpiresIn { get; }

        public AccessToken(string token, int expiresIn)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("message", nameof(token));

            Token = token;
            ExpiresIn = expiresIn;
        }
    }
}
