using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Blueprints.Application.Requests;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Commands
{
    public class AddCardsFromFile
    {
        public class CommandHandler : RequestHandlerBase<Command, Unit>
        {
            private readonly IOwnerRepository _repository;
            private readonly ISequenceGenerator _sequenceGenerator;
            private readonly IHashIdsService _hash;

            public CommandHandler(IOwnerRepository repository,
                ISequenceGenerator sequenceGenerator, IHashIdsService hash)
            {
                _repository = repository;
                _sequenceGenerator = sequenceGenerator;
                _hash = hash;
            }

            public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = OwnerId.Restore(request.UserId);
                var groupId = GroupId.Restore(_hash.GetLongId(request.GroupId));

                var owner = await _repository.Get(ownerId, cancellationToken);

                var itemLines = request.Content.Split(request.ItemSeparator);
                foreach (var itemLine in itemLines)
                {
                    var elements = itemLine.Split(request.ElementSeparator);

                    var frontValueIndex = request.ItemsOrder.FindIndex(x => x == "FV");
                    var frontExampleIndex = request.ItemsOrder.FindIndex(x => x == "FE");
                    var backValueIndex = request.ItemsOrder.FindIndex(x => x == "BV");
                    var backExampleIndex = request.ItemsOrder.FindIndex(x => x == "BE");

                    var frontValue = elements[frontValueIndex];
                    var backValue = elements[backValueIndex];

                    var frontExample = frontExampleIndex >= 0 ? elements[frontExampleIndex] : string.Empty;
                    var backExample = backExampleIndex >= 0 ? elements[backExampleIndex] : string.Empty;

                    owner.AddCard(
                        groupId,
                        Label.Create(frontValue),
                        Label.Create(backValue),
                        frontExample,
                        backExample,
                        Comment.Create(string.Empty),
                        Comment.Create(string.Empty),
                        _sequenceGenerator
                    );
                }

                await _repository.Update(owner, cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public string GroupId { get; set; }
            public string Content { get; set; }
            public string ItemSeparator { get; set; }
            public string ElementSeparator { get; set; }
            public List<string> ItemsOrder { get; set; }
        }
    }
}