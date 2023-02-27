
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.AddGroup;

public class SimpleGroup : AddGroupSuccessContext
{
    public override Application.Features.Groups.AddGroup.Command GivenRequest { get; } = new()
    {
        UserId = CardsTestBase.UserId,
        GroupName = "groupName",
        Front = 1,
        Back = 2
    };

    public override Group ExpectedGroup { get; } = new()
    {
        Back = 2, 
        Front = 1,
        Name = "groupName",
        OwnerId = CardsTestBase.UserId,
    };
}