using System;
using System.Collections.Generic;
using Api.Model.Requests.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.UpdateCard.Contexts;

public abstract class UpdateCardContext
{
    public Owner GivenOwner { get; }
    public Card GivenCard { get; }

    public virtual Api.Model.Requests.UpdateCard GivenCommand => new(
        new CardSide("NewFrontValue", "NewFrontExample", true, true),
        new CardSide("NewBackValue", "NewBackExample", true, true),
        "NewComment");

    protected UpdateCardContext()
    {
        var owner = DataBuilder.SampleUser().Build();
        var group = DataBuilder.SampleGroup().Build();
        owner.Groups.Add(group);

        GivenCard = new Card
        {
            Front = DataBuilder.FrontSide().Build(),
            Back = DataBuilder.BackSide().Build(),
            Details = new List<Detail>
            {
                DataBuilder.Detail().With(x => x.SideType = 1).With(x => x.IsQuestion = false)
                    .With(x => x.NextRepeat = null).Build(),
                DataBuilder.Detail().With(x => x.SideType = 2).With(x => x.IsQuestion = false)
                    .With(x => x.NextRepeat = null).Build()
            }
        };
        group.Cards.Add(GivenCard);
        GivenOwner = owner;
    }

    public virtual Side ExpectedFront { get; } = new()
    {
        Label = "NewFrontValue", Example = "NewFrontExample"
    };

    public virtual Side ExpectedBack { get; } = new()
    {
        Label = "NewBackValue", Example = "NewBackExample"
    };

    public virtual IEnumerable<Detail> ExpectedDetails { get; } = new[]
    {
        new Detail
        {
            IsQuestion = true, SideType = 1, Counter = 2, Drawer = 2, IsTicked = true,
            NextRepeat = new DateTime().ToUniversalTime()
        },
        new Detail
        {
            IsQuestion = true, SideType = 2, Counter = 2, Drawer = 2, IsTicked = true,
            NextRepeat = new DateTime().ToUniversalTime()
        }
    };
}