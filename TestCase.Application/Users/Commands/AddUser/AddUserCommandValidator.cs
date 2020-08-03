using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Users.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(u => u.Email).NotNull().MaximumLength(50);
            RuleFor(u => u.UserName).NotNull().MaximumLength(50);
        }
    }
}
