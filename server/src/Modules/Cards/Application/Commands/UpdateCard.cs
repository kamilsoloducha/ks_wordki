using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Application.Services;
using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FluentValidation;

namespace Cards.Application.Commands;

public class UpdateCard
{
    internal class CommandHandler : RequestHandlerBase<Command, Response>
    {
        private readonly IOwnerRepository _repository;
        private readonly ISequenceGenerator _sequenceGenerator;
        private readonly IHashIdsService _hash;

        public CommandHandler(IOwnerRepository repository, ISequenceGenerator sequenceGenerator, IHashIdsService hash)
        {
            _repository = repository;
            _sequenceGenerator = sequenceGenerator;
            _hash = hash;
        }

        public override async Task<ResponseBase<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);
            var owner = await _repository.Get(ownerId, cancellationToken);

            var groupId = GroupId.Restore(_hash.GetLongId(request.GroupId));
            var cardId = CardId.Restore(_hash.GetLongId(request.CardId));
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
            return ResponseBase<Response>.Create(new Response { CardId = _hash.GetHash(result.Value) });
        }
    }

    public class Command : RequestBase<Response>
    {
        public Guid UserId { get; set; }
        public string GroupId { get; set; }
        public string CardId { get; set; }
        public CardSide Front { get; set; }
        public CardSide Back { get; set; }
        public string Comment { get; set; }
    }

    public class CardSide
    {
        public string Value { get; set; }
        public string Example { get; set; }
        public bool? IsUsed { get; set; }
        public bool IsTicked { get; set; }
    }

    public class Response
    {
        public string CardId { get; set; }
    }

    internal class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
            RuleFor(x => x.GroupId).NotEmpty();
            RuleFor(x => x.CardId).NotEmpty();
            RuleFor(x => x.Front.Value).NotEmpty();
            RuleFor(x => x.Back.Value).NotEmpty();
        }
    }
}