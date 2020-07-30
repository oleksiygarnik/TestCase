using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.WebAPI.Controllers.Abstract
{
    public abstract class MediatorController : Controller
    {
        protected readonly IMediator _mediator;

        public MediatorController(IMediator mediatr)
        {
            _mediator = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }

        protected async Task<IActionResult> ExecuteQuery<T>(IRequest<T> query)
        {
            if (query == null)
                return BadRequest();

            var entity = await _mediator.Send(query);

            if (entity == null)
                return NotFound();

            return Json(entity);
        }
    }
}
