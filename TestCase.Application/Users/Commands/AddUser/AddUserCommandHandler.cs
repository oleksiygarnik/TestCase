using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Application.Auth;
using TestCase.Application.Users.Queries;
using TestCase.Domain;

namespace TestCase.Application.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly IUnitOfWork _work;
        private readonly IEntityRepository _repository;

        public AddUserCommandHandler(IUnitOfWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
            _repository = work.EntityRepository;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var salt = SecurityHelper.GetRandomBytes();

            var user = new User(request.Email, request.UserName, SecurityHelper.HashPassword(request.Password, salt), Convert.ToBase64String(salt));

            await _repository.Add(user);
            await _work.Commit(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };
        }
    }
}
