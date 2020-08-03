using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Users.Queries;

namespace TestCase.Infrastructure.QueryHandlers.Users
{
    public class UserByIdQueryHandler : IRequestHandler<UserByIdQuery, UserDto>
    {
        private readonly TransactionsContext _context;

        public UserByIdQueryHandler(TransactionsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserDto> Handle(UserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (user is null)
                throw new NotFoundEntityException(nameof(user));

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
