using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Groups
{
    public abstract class DeleteGroup
    {
        internal class CommandHandler : RequestHandlerBase<Command, Unit>
        {
            private readonly IOwnerRepository _repository;

            public CommandHandler(IOwnerRepository repository)
            {
                _repository = repository;
            }

            public override async Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = UserId.Restore(request.UserId);
                var owner = await _repository.Get(ownerId, cancellationToken);

                var group = owner.Groups.FirstOrDefault(x => x.Id == request.GroupId);
                if (group is null) return ResponseBase<Unit>.Create(Unit.Value);

                group.Remove();
            
                await _repository.Update(cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public record Command(Guid UserId, long GroupId) : IRequest<ResponseBase<Unit>>;
    }
}