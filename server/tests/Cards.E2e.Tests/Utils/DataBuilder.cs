using System;
using System.Collections.Generic;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;
using FluentAssertions;

namespace Cards.E2e.Tests.Utils;

public static class DataBuilder
{
    public static ISingleObjectBuilder<Owner> SampleUser() => Builder<Owner>.CreateNew()
        .With(x => x.Id = 0)
        .With(x => x.UserId = CardsTestBase.UserId);

    public static ISingleObjectBuilder<Group> SampleGroup() => Builder<Group>.CreateNew()
        .With(x => x.Id = 0)
        .With(x => x.Front = "1")
        .With(x => x.Back = "2")
        .With(x => x.ParentId = null)
        .With(x => x.Name = "GroupName");

    public static ISingleObjectBuilder<Card> SampleCard() => Builder<Card>.CreateNew()
        .With(x => x.Id = 0)
        .With(x => x.FrontId = 0)
        .With(x => x.BackId = 0)
        .With(x => x.Front = FrontSide().Build())
        .With(x => x.Back = BackSide().Build())
        .With(x => x.Details = new List<Detail>()
        {
            Detail().With(d => d.SideType = 1).Build(),
            Detail().With(d => d.SideType = 2).Build()
        });

    public static ISingleObjectBuilder<Side> FrontSide() => Builder<Side>.CreateNew()
        .With(x => x.Id = 0)
        .With(x => x.Label = "FrontValue")
        .With(x => x.Example = "FrontExample");

    public static ISingleObjectBuilder<Side> BackSide() => Builder<Side>.CreateNew()
        .With(x => x.Id = 0)
        .With(x => x.Label = "BackValue")
        .With(x => x.Example = "BackExample");

    public static ISingleObjectBuilder<Detail> FrontDetails() => Detail().With(x => x.SideType = 1);
    public static ISingleObjectBuilder<Detail> BackDetails() => Detail().With(x => x.SideType = 2);

    public static ISingleObjectBuilder<Detail> Detail() => Builder<Detail>.CreateNew()
        .With(x => x.Counter = 2)
        .With(x => x.Drawer = 2)
        .With(x => x.IsTicked = true)
        .With(x => x.IsQuestion = true)
        .With(x => x.NextRepeat = new DateTime(2022, 2, 2));
}