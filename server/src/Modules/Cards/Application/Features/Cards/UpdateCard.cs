using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Cards;

public abstract class UpdateCard
{
    internal class CommandHandler : RequestHandlerBase<Command, long>
    {
        private readonly IOwnerRepository _repository;
        private readonly ISequenceGenerator _sequenceGenerator;

        public CommandHandler(IOwnerRepository repository, ISequenceGenerator sequenceGenerator)
        {
            _repository = repository;
            _sequenceGenerator = sequenceGenerator;
        }

        public override async Task<ResponseBase<long>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);
            var owner = await _repository.Get(ownerId, cancellationToken);

            var groupId = GroupId.Restore(request.GroupId);
            var cardId = CardId.Restore(request.CardId);
            var frontValue = Label.Create(request.Front.Value);
            var backValue = Label.Create(request.Back.Value);

            var frontComment = Comment.Create(request.Comment);
            var backComment = Comment.Create(request.Comment);

            var command = new UpdateCardCommand
            {
                Front = new UpdateCardCommand.Side
                {
                    Value = frontValue,
                    Example = new Example(request.Front.Example),
                    Comment = frontComment,
                    IncludeLesson = request.Front.IsUsed,
                    IsTicked = request.Front.IsTicked
                },
                Back = new UpdateCardCommand.Side
                {
                    Value = backValue,
                    Example = new Example(request.Back.Example),
                    Comment = backComment,
                    IncludeLesson = request.Back.IsUsed,
                    IsTicked = request.Back.IsTicked
                }
            };

            var result = owner.UpdateCard(groupId, cardId, command, _sequenceGenerator);

            await _repository.Update(owner, cancellationToken);
            return ResponseBase<long>.Create(result.Value);
        }
    }

    public record Command(
            Guid UserId,
            long GroupId,
            long CardId,
            CardSide Front,
            CardSide Back,
            string Comment)
        : IRequest<ResponseBase<long>>;

    public record CardSide(string Value, string Example, bool? IsUsed, bool IsTicked);
}