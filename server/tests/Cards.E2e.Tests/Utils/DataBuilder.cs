using System;
using System.Collections.Generic;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;
using NUnit.Framework.Internal;

namespace Cards.E2e.Tests.Utils;

public static class DataBuilder
{
    public static ISingleObjectBuilder<Owner> EmptyOwner() => Builder<Owner>.CreateNew()
        .With(x => x.Id = CardsTestBase.UserId);

    public static ISingleObjectBuilder<Group> EmptyGroup() => Builder<Group>.CreateNew()
        .With(x => x.Front = 1)
        .With(x => x.Back = 2)
        .With(x => x.Name = "GroupName");

    public static ISingleObjectBuilder<Side> FrontSide() => Builder<Side>.CreateNew()
        .With(x => x.Value = "FrontValue")
        .With(x => x.Example = "FrontExample")
        .With(x => x.Type = 1);

    public static ISingleObjectBuilder<Side> BackSide() => Builder<Side>.CreateNew()
        .With(x => x.Value = "BackValue")
        .With(x => x.Example = "BackExample")
        .With(x => x.Type = 2);

    public static ISingleObjectBuilder<Detail> Detail() => Builder<Detail>.CreateNew()
        .With(x => x.Comment = "Comment")
        .With(x => x.Counter = 2)
        .With(x => x.Drawer = 2)
        .With(x => x.IsTicked = true)
        .With(x => x.LessonIncluded = true)
        .With(x => x.NextRepeat = new DateTime(2022, 2, 2));

    public static Owner AddGroup(this Owner owner)
    {
        var group = EmptyGroup().Build();
        owner.Groups.Add(group);
        return owner;
    }
    
    
}