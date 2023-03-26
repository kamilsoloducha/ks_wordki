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
            var ownerId = UserId.Restore(request.UserId);

            var group = await _repository.GetGroup(ownerId, request.GroupId, cancellationToken);

            group.IncludeToLesson(request.Count, request.Language);

            await _repository.Update(cancellationToken);

            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(Guid UserId, long GroupId, int Count, string Language) : IRequest<ResponseBase<Unit>>;
}