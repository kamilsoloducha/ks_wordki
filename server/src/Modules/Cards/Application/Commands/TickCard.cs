using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Commands
{
    public class TickCard
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
                var sideId = SideId.Restore(request.SideId);

                var side = await _repository.Get(ownerId, sideId, cancellationToken);
                if (side is null) return ResponseBase<Unit>.Create("side is null");

                if (side.IsTicked) return ResponseBase<Unit>.Create(Unit.Value);

                side.Tick();

                await _repository.Update(cancellationToken);
                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public long SideId { get; set; }

        }
    }
}