using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using TestCase.Application.Auth;
using TestCase.Application.Auth.DTO;
using TestCase.Application.Users.Commands.AddUser;
using TestCase.WebAPI.Controllers.Abstract;

namespace TestCase.WebAPI.Controllers
{
    [Route("api/register")]
    [AllowAnonymous]
    [ApiController]
    public class RegisterController : MediatorController
    {
        private readonly AuthService _authService;

        public RegisterController(AuthService authService, IMediator mediatr) : base(mediatr)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// Register and auth user in system(Sign up and sign in)
        /// </summary>
        /// <param name="user"></param>
        /// <returns>AuthUserDto that contains information about user(UserDto) and token(AccessTokenDto)</returns>
        /// <response code="200">Returns AuthUserDto that contains information about user(UserDto) and token(AccessTokenDto) </response>
        /// <response code="400">If dto is null</response>
        /// <response code="404">If server can't find neccessary user with same data(password, username)</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(AuthUserDto))]
        public async Task<IActionResult> Post([FromBody] UserRegisterDto user)
        {
            var command = new AddUserCommand()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };

            var createdUser = await _mediator.Send(command);

            if (createdUser is null)
                return BadRequest();

            var token = await _authService.GenerateAccessToken(createdUser.Id, createdUser.UserName, createdUser.Email);

            var result = new AuthUserDto
            {
                User = createdUser,
                Token = token
            };

            return Created($"api/users/{createdUser.Id}", result);
        }
    }
}
