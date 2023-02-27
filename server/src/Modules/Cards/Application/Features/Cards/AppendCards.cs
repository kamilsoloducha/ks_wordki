using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Cards;

public abstract class AppendCards
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
            var ownerId = OwnerId.Restore(request.UserId);
            var groupId = GroupId.Restore(request.GroupId);

            var owner = await _repository.Get(ownerId, cancellationToken);
            owner.IncludeToLesson(groupId, request.Count, request.Language);

            await _repository.Update(owner, cancellationToken);

            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(Guid UserId, long GroupId, int Count, int Language) : IRequest<ResponseBase<Unit>>;
}