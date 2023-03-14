using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Groups
{
    public abstract class AddGroup
    {
        internal class CommandHandler : RequestHandlerBase<Command, long>
        {
            private readonly IOwnerRepository _repository;

            public CommandHandler(IOwnerRepository repository)
            {
                _repository = repository;
            }

            public override async Task<ResponseBase<long>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = UserId.Restore(request.UserId);
                var owner = await _repository.Get(userId, cancellationToken);
                if (owner is null) return ResponseBase<long>.CreateError("cardsSet is null");

                var groupName = new GroupName(request.GroupName);

                var group = owner.CreateGroup(groupName, request.Front, request.Back);

                await _repository.Update(cancellationToken);
                return ResponseBase<long>.Create(group.Id);
            }
        }

        public record Command(Guid UserId, string GroupName, string Front, string Back) : IRequest<ResponseBase<long>>;
    }
}