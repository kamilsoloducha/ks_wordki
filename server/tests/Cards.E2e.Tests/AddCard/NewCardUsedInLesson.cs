using System;
using System.Collections.Generic;
using Api.Model.Requests.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.AddCard;

public class NewCardUsedInLesson : AddCardSuccessContext
{
    public override Api.Model.Requests.AddCard GivenRequest { get; } = new(
        new CardSide("FrontValue", "FrontExample", true, false),
        new CardSide("BackValue", "BackExample", true, false),
        "Comment");

    public override Group GivenGroup { get; } = new()
    {
        Id = 1,
        Back = "1",
        Front = "2",
        Name = "Name",
    };

    public override IReadOnlyCollection<Card> ExpectedCards { get; } = new[]
    {
        new Card
        {
            Front = new Side { Label = "FrontValue", Example = "FrontExample" },
            Back = new Side { Label = "BackValue", Example = "BackExample" }
        }
    };

    public override IReadOnlyCollection<Detail> ExpectedDetails { get; } = new[]
    {
        new Detail { IsQuestion = true, IsTicked = false, Drawer = 0, Counter = 0, NextRepeat = new DateTime() },
        new Detail { IsQuestion = true, IsTicked = false, Drawer = 0, Counter = 0, NextRepeat = new DateTime() }
    };
}

public class NewCardNotUsedInLesson : AddCardSuccessContext
{
    public override Api.Model.Requests.AddCard GivenRequest { get; } = new(
        new CardSide("FrontValue", "FrontExample", false, false),
        new CardSide("BackValue", "BackExample", false, false),
        "Comment");

    public override Group GivenGroup { get; } = new()
    {
        Id = 1,
        Back = "1",
        Front = "2",
        Name = "Name",
    };

    public override IReadOnlyCollection<Card> ExpectedCards { get; } = new[]
    {
        new Card
        {
            Front = new Side { Label = "FrontValue", Example = "FrontExample" },
            Back = new Side { Label = "BackValue", Example = "BackExample" }
        }
    };

    public override IReadOnlyCollection<Detail> ExpectedDetails { get; } = new[]
    {
        new Detail { IsQuestion = false, Drawer = 0, Counter = 0, NextRepeat = null },
        new Detail { IsQuestion = false, Drawer = 0, Counter = 0, NextRepeat = null }
    };
}