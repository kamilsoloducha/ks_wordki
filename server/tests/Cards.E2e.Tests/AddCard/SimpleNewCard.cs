using System.Collections.Generic;
using Cards.Domain.Enums;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.AddCard;

public class SimpleNewCard : AddCardSuccessContext
{
    public override Application.Commands.AddCard.Command GivenRequest { get; } = new()
    {
        UserId = CardsTestBase.UserId,
        GroupId = "1",
        Front = new Application.Commands.AddCard.CardSide
            { Value = "FrontValue", Example = "FrontExample", IsUsed = false },
        Back = new Application.Commands.AddCard.CardSide
            { Value = "BackValue", Example = "BackExample", IsUsed = false },
        Comment = "Comment"
    };

    public override Group GivenGroup { get; } = new()
    {
        Id = 1,
        Back = 1,
        Front = 2,
        Name = "Name",
    };

    public override IReadOnlyCollection<Card> ExpectedCards { get; } = new[]
    {
        new Card
        {
            Front = new Side { Type = (int)SideType.Front, Value = "FrontValue", Example = "FrontExample" },
            Back = new Side { Type = (int)SideType.Back, Value = "BackValue", Example = "BackExample" },
            IsPrivate = true
        }
    };

    public override IReadOnlyCollection<Detail> ExpectedDetails { get; } = new[]
    {
        new Detail { LessonIncluded = false, IsTicked = false, Drawer = 0, Counter = 0, Comment = "Comment" },
        new Detail { LessonIncluded = false, IsTicked = false, Drawer = 0, Counter = 0, Comment = "Comment" }
    };
}