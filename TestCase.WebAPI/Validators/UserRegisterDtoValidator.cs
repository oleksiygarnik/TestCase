using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Application.Auth.DTO;

namespace TestCase.WebAPI.Validators
{
    public sealed class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                    .WithMessage("Username is mandatory.")
                .MinimumLength(3)
                    .WithMessage("Username should be minimum 3 character.");

            RuleFor(u => u.Email)
                .EmailAddress();

            RuleFor(u => u.Password)
                .Length(4, 16)
                .WithMessage("Password must be from 4 to 16 characters.");
        }
    }
}
