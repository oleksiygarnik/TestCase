using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionStatusCommandValidator : AbstractValidator<UpdateTransactionStatusCommand>
    {
        public UpdateTransactionStatusCommandValidator()
        {
            RuleFor(t => t.Id).GreaterThan(0);
        }
    }
}
