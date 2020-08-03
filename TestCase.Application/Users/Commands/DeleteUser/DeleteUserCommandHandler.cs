using Domain;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCase.Domain;

namespace TestCase.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _work;
        private readonly IEntityRepository _repository;

        public DeleteUserCommandHandler(IUnitOfWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
            _repository = work.EntityRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var userForDelete = await _repository.SingleOrDefault<User>(t => t.Id == request.Id, cancellationToken);

            if (userForDelete is null)
                throw new NotFoundEntityException(nameof(userForDelete)); //return Unit.Value;

            await _repository.Remove(userForDelete);
            await _work.Commit();

            return Unit.Value;
        }
    }
}
