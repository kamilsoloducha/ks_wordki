using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Commands
{
    public class AppendCards
    {

        internal class CommandHandler : RequestHandlerBase<Command, Unit>
        {

            private readonly ISetRepository _repository;

            public CommandHandler(ISetRepository repository)
            {
                _repository = repository;
            }

            public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = UserId.Restore(request.UserId);
                var groupId = GroupId.Restore(request.GroupId);

                var set = await _repository.Get(userId, cancellationToken);
                set.AppendCards(groupId, request.Count, request.Langauges);

                await _repository.Update(set, cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public Guid GroupId { get; set; }
            public int Count { get; set; }
            public int Langauges { get; set; }
        }
    }
}