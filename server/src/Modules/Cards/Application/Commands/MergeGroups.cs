using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain2;
using MediatR;

namespace Cards.Application.Commands
{
    public class MergeGroups
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
                var ownerId = OwnerId.Restore(request.UserId);
                var owner = await _repository.Get(ownerId, cancellationToken);

                var groupIds = request.GroupIds.Select(x => GroupId.Restore(x));
                owner.MergeGroups(groupIds);

                await _repository.Update(owner, cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public IEnumerable<long> GroupIds { get; set; }
        }
    }
}