using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using TestCase.Application.Auth;
using TestCase.Application.Auth.DTO;

namespace TestCase.WebAPI.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Auth user in system(Sign in)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AuthUserDto that contains information about user(UserDto) and token(AccessTokenDto)</returns>
        /// <response code="200">Returns AuthUserDto that contains information about user(UserDto) and token(AccessTokenDto) </response>
        /// <response code="400">If dto is null</response>
        /// <response code="404">If server can't find neccessary user with same data(password, username)</response>
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(AuthUserDto))]
        public async Task<ActionResult<AuthUserDto>> Login(UserLoginDto dto)
        {
            if (dto is null)
                return BadRequest();

            return Ok(await _authService.Authorize(dto));
        }
    }
}
