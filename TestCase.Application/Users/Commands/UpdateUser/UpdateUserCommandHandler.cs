using Domain;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Users.Queries;
using TestCase.Domain;

namespace TestCase.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUnitOfWork _work;
        private readonly IEntityRepository _repository;

        public UpdateUserCommandHandler(IUnitOfWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
            _repository = _work.EntityRepository;
        }
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = await _repository.SingleOrDefault<User>(t => t.Id == request.Id, cancellationToken);

            if (user is null)
                throw new NotFoundEntityException(nameof(user));

            user.ChangeUsername(request.Username);
            user.ChangeEmail(request.Email);

            await _work.Commit();

            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
        }

    }
}
