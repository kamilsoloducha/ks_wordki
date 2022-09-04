using Cards.E2e.Tests.Models.Cards;

namespace Cards.E2e.Tests.AddGroup;

public class SimpleGroup : AddGroupSuccessContext
{
    public override Application.Commands.AddGroup.Command GivenRequest { get; } = new()
    {
        UserId = CardsTestBase.OwnerId,
        GroupName = "groupName",
        Front = 1,
        Back = 2
    };

    public override Group ExpectedGroup { get; } = new()
    {
        Back = 2, 
        Front = 1,
        Name = "groupName",
        OwnerId = CardsTestBase.OwnerId,
    };
}