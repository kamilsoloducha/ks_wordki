using Cards.E2e.Tests.Models.Cards;

namespace Cards.E2e.Tests.AddGroup;

public abstract class AddGroupSuccessContext
{
    public abstract Application.Commands.AddGroup.Command GivenRequest { get; }

    public abstract Group ExpectedGroup { get; }
}