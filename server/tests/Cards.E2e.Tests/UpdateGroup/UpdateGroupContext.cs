using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.UpdateGroup;

public abstract class UpdateGroupContext
{
    public const int GroupId = 1;
    public Owner GivenOwner { get; }

    public Application.Commands.UpdateGroup.Command GivenCommand => new()
    {
        UserId = CardsTestBase.UserId,
        GroupId = GroupId.ToString(),
        GroupName = "NewGroupName",
        Front = 3,
        Back = 4
    };

    protected UpdateGroupContext()
    {
        var owner = DataBuilder.EmptyOwner().Build();
        var group = DataBuilder.EmptyGroup().With(x => x.Id = GroupId).Build();
        owner.Groups.Add(group);

        var card = new Card
        {
            Front = DataBuilder.FrontSide().With(x => x.Id = 1).Build(),
            Back = DataBuilder.BackSide().With(x => x.Id = 2).Build(),
            Id = 1,
            FrontId = 1,
            BackId = 2,
            IsPrivate = true
        };
        group.Cards.Add(card);

        owner.Details.Add(
            DataBuilder.Detail().With(x => x.Id = 1).With(x => x.SideId = 1).With(x => x.OwnerId = owner.Id)
                .Build());

        owner.Details.Add(
            DataBuilder.Detail().With(x => x.Id = 2).With(x => x.SideId = 2).With(x => x.OwnerId = owner.Id)
                .Build());

        GivenOwner = owner;
    }

    public Group ExpectedGroup => new ()
    {
        Id = 1,
        Front = GivenCommand.Front,
        Back = GivenCommand.Back,
        Name = GivenCommand.GroupName,
    };
}