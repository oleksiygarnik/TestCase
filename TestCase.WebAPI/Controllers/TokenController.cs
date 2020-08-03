using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Application.Auth;
using TestCase.Application.Auth.DTO;
using TestCase.WebAPI.Extensions;

namespace TestCase.WebAPI.Controllers
{
    [Route("api/token")]
    [Authorize]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AuthService _authService;

        public TokenController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Refresh jwt 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>AccessTokenDto</returns>
        /// <response code="200">Returns AccessTokenDto that contains informations about token and time when expires</response>
        /// <response code="400">If the dto is null</response>
        /// <response code="404">If server can't find neccessary resource</response>
        [HttpPost("refresh")]
        public async Task<ActionResult<AccessTokenDto>> Refresh([FromBody] RefreshTokenDto dto)
        {
            return Ok(await _authService.RefreshToken(dto));
        }

        /// <summary>
        /// Revoke jwt (logout) 
        /// </summary>
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenDto dto)
        {
            var userId = this.GetUserIdFromToken();
            await _authService.RevokeRefreshToken(dto.RefreshToken, userId);

            return Ok();
        }
    }
}
