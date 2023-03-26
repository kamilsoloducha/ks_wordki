using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.AddGroup;

public class SimpleGroup : AddGroupSuccessContext
{
    public override Api.Model.Requests.AddGroup GivenRequest { get; } = new("GroupName", "1", "2");

    public override Group ExpectedGroup { get; } = new()
    {
        Name = "GroupName",
        Front = "1",
        Back = "2",
    };
}