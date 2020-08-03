using TestCase.Application.Users.Queries;

namespace TestCase.Application.Auth.DTO
{
    public sealed class UserRegisterDto : UserDto
    {
        public string Password { get; set; }
    }
}
