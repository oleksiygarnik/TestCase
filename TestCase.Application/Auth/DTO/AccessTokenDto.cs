using TestCase.Application.Auth.Common;

namespace TestCase.Application.Auth.DTO
{
    public sealed class AccessTokenDto
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }

        public AccessTokenDto(AccessToken accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
