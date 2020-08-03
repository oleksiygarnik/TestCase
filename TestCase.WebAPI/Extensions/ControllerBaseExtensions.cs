using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Application.Auth.Exceptions;

namespace TestCase.WebAPI.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static int GetUserIdFromToken(this ControllerBase controller)
        {
            var claimsUserId = controller.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

            if (string.IsNullOrEmpty(claimsUserId))
                throw new InvalidTokenException("access");

            return int.Parse(claimsUserId);
        }
    }
}
