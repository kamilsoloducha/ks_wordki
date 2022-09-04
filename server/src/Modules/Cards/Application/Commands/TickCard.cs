using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Application.Services;
using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Cards.Application.Commands;

public class TickCard
{
    internal class CommandHandler : RequestHandlerBase<Command, Unit>
    {
        private readonly IOwnerRepository _repository;
        private readonly IHashIdsService _hash;

        public CommandHandler(IOwnerRepository repository, IHashIdsService hash)
        {
            _repository = repository;
            _hash = hash;
        }

        public override async Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);
            var sideId = SideId.Restore(_hash.GetLongId(request.SideId));

            var side = await _repository.Get(ownerId, sideId, cancellationToken);

            if (side.IsTicked) return ResponseBase<Unit>.Create(Unit.Value);

            side.Tick();

            await _repository.Update(cancellationToken);
            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public class Command : RequestBase<Unit>
    {
        public Guid UserId { get; set; }
        public string SideId { get; set; }
    }

    internal class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
            RuleFor(x => x.SideId).NotEmpty();
        }
    }
}