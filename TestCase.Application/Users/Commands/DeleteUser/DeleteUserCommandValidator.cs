using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(u => u.Id).GreaterThan(0);
        }
    }
}
