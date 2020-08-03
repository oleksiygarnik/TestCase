using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Application.Auth.DTO;

namespace TestCase.WebAPI.Validators
{
    public sealed class RevokeRefreshTokenDtoValidator : AbstractValidator<RevokeRefreshTokenDto>
    {
        public RevokeRefreshTokenDtoValidator()
        {
            RuleFor(r => r.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token can not be empty.");
        }
    }
}
