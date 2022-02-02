using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Commands
{
    public class AddCardSide
    {
        public class CommandHandler : RequestHandlerBase<Command, Unit>
        {
            private readonly IOwnerRepository _repository;

            public CommandHandler(IOwnerRepository repository)
            {
                _repository = repository;
            }

            public override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(ResponseBase<Unit>.Create(Unit.Value));
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public IEnumerable<CardSideId> CardSideIds { get; set; }
        }

        public class CardSideId
        {
            public Guid CardId { get; set; }
        }
    }
}