using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Commands
{
    public class ConnectGroups
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
                var set = await _repository.Get(userId, cancellationToken);

                var groupIds = request.GroupIds.Select(x => GroupId.Restore(x));
                set.ConnectGroups(groupIds);

                await _repository.Update(set, cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public IEnumerable<Guid> GroupIds { get; set; }
        }
    }
}