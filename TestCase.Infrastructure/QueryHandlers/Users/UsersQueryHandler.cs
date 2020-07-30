using Application;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Users.Queries;
using TestCase.Infrastructure.QueryHandlers.Extensions;

namespace TestCase.Infrastructure.QueryHandlers.Users
{
    public class UsersQueryHandler : IRequestHandler<UsersQuery, PagedResponse<UserDto>>
    {
        private readonly TransactionsContext _context;

        public UsersQueryHandler(TransactionsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<PagedResponse<UserDto>> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var users = await _context.Users
                .ByUsername(request.Username)
                .ByEmail(request.Email)
                .Select(user => new UserDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                })
                .Paginate(request, cancellationToken);

            if (users == null)
                throw new NotFoundEntityException(nameof(users));

            var options = new PageOptions(users.Current, users.Size);

            return new PagedResponse<UserDto>(options, users.Total, users.Items.ToArray());

        }
    }
}
