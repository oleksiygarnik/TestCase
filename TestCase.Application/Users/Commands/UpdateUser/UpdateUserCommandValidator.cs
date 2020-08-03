using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.Id).GreaterThan(0);
        }
    }
}
