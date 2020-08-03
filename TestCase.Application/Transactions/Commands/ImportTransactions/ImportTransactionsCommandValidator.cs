using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCase.Application.Transactions.Commands.ImportTransactions
{
    public class ImportTransactionsCommandValidator : AbstractValidator<ImportTransactionsCommand>
    {
        public ImportTransactionsCommandValidator()
        {
            RuleFor(t => t.File).NotNull();
        }
    }
}
