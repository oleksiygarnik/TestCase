using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class PagedRequestValidator : AbstractValidator<PagedRequest>
    {
        public PagedRequestValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(0);
            RuleFor(r => r.PageSize).GreaterThanOrEqualTo(0);
        }
    }
}
