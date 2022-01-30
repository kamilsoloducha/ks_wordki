using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain2;
using MediatR;

namespace Cards.Application.Commands
{
    public class AppendCards
    {
        internal class CommandHandler : RequestHandlerBase<Command, Unit>
        {

            private readonly ICardsRepository _repository;

            public CommandHandler(ICardsRepository repository)
            {
                _repository = repository;
            }

            public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = OwnerId.Restore(request.OwnerId);
                var groupId = GroupId.Restore(request.GroupId);

                var owner = await _repository.Get(ownerId, cancellationToken);
                owner.IncludeToLesson(groupId, request.Count, request.Langauges);

                await _repository.Update(owner, cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid OwnerId { get; set; }
            public long GroupId { get; set; }
            public int Count { get; set; }
            public int Langauges { get; set; }
        }
    }
}