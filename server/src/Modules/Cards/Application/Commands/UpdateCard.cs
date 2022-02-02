using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using FluentValidation;

namespace Cards.Application.Commands
{
    public class UpdateCard
    {
        internal class CommandHandler : RequestHandlerBase<Command, Response>
        {
            private readonly IOwnerRepository _repository;
            private readonly ISequenceGenerator _sequenceGenerator;

            public CommandHandler(IOwnerRepository repository, ISequenceGenerator sequenceGenerator)
            {
                _repository = repository;
                _sequenceGenerator = sequenceGenerator;
            }

            public async override Task<ResponseBase<Response>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = OwnerId.Restore(request.UserId);
                var owner = await _repository.Get(ownerId, cancellationToken);
                if (owner is null) return ResponseBase<Response>.Create("set is null");

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
                        Example = request.Front.Example,
                        Comment = frontComment,
                        IncludeLesson = request.Front.IsUsed,
                        IsTicked = request.Front.IsTicked
                    },
                    Back = new UpdateCardCommand.Side
                    {
                        Value = backValue,
                        Example = request.Back.Example,
                        Comment = backComment,
                        IncludeLesson = request.Back.IsUsed,
                        IsTicked = request.Back.IsTicked
                    }
                };

                var result = owner.UpdateCard(groupId, cardId, command, _sequenceGenerator);

                await _repository.Update(owner, cancellationToken);
                return ResponseBase<Response>.Create(new Response { CardId = result.Value });
            }
        }

        public class Command : RequestBase<Response>
        {
            public Guid UserId { get; set; }
            public long GroupId { get; set; }
            public long CardId { get; set; }
            public CardSide Front { get; set; }
            public CardSide Back { get; set; }
            public string Comment { get; set; }
        }

        public class CardSide
        {
            public string Value { get; set; }
            public string Example { get; set; }
            public bool IsUsed { get; set; }
            public bool IsTicked { get; set; }
        }

        public class Response
        {
            public long CardId { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
                RuleFor(x => x.GroupId).Must(x => x != 0);
                RuleFor(x => x.CardId).Must(x => x != 0);
                RuleFor(x => x.Front.Value).NotEmpty();
                RuleFor(x => x.Back.Value).NotEmpty();
            }
        }
    }
}