using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Commands
{
    public class AddCardsFromFile
    {
        public class CommandHandler : RequestHandlerBase<Command, Unit>
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

                    var newCard = set.AddCard(
                        groupId,
                        SideLabel.Create(frontValue),
                        frontExample,
                        false,
                        SideLabel.Create(backValue),
                        backExample,
                        false,
                        string.Empty
                    );
                }

                await _repository.Update(set, cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public Guid GroupId { get; set; }
            public string Content { get; set; }
            public string ItemSeparator { get; set; }
            public string ElementSeparator { get; set; }
            public List<string> ItemsOrder { get; set; }
        }
    }
}