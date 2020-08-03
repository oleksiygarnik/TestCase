using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TestCase.Application.Users.Commands.AddUser;
using TestCase.Application.Users.Commands.DeleteUser;
using TestCase.Application.Users.Commands.UpdateUser;
using TestCase.Application.Users.Queries;
using TestCase.WebAPI.Controllers.Abstract;

namespace TestCase.WebAPI.Controllers
{
    [Route("api/users")]
    public class UserController : MediatorController
    {
        public UserController(IMediator mediator) : base(mediator)
        {

        }

        /// <summary>
        /// Gets collection of users from database
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Collection of users</returns>
        /// <response code="200">Returns collection of users from database</response>
        /// <response code="400">If the query is null</response>
        /// <response code="404">If server can't find neccessary resource</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(IEnumerable<UserDto>))]
        public Task<IActionResult> GetUsers(UsersQuery query) => ExecuteQuery(query);


        /// <summary>
        /// Gets user by id from database
        /// </summary>
        /// <param name="query"></param>
        /// <returns>User by id(integer type)</returns>
        /// <response code="200">Returns user by id(integer type) from database</response>
        /// <response code="400">If the query is null</response>
        /// <response code="404">If server can't find neccessary user with same id</response>
        [HttpGet("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(UserDto))]
        [AllowAnonymous]
        public Task<IActionResult> GetUserById(UserByIdQuery query) => ExecuteQuery(query);

        /// <summary>
        /// Delete user by id from database
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status code 204 - NoContent</returns>
        /// <response code="204">Returns status code 204 if everything okey</response>
        /// <response code="400">If the command is null</response>
        /// <response code="404">If server can't find neccessary user with same id</response>
        [HttpDelete("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
        {
            if (command is null)
                return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Update user in database
        /// </summary>
        /// <param name="command"></param>
        /// <returns>User that was updated with new values</returns>
        /// <response code="200">Returns user that was updated with new values</response>
        /// <response code="400">If the command is null</response>
        /// <response code="404">If server can't find neccessary user with same id</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces(typeof(UserDto))]
        public async Task<IActionResult> UpdateUser([FromBody]  UpdateUserCommand command)
        {
            if (command is null)
                return BadRequest();

            return Json(await _mediator.Send(command));
        }

    }
}
