using System;
using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using Domain.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetRepeats.Contexts;

internal abstract class GroupId : GetRepeatsContext
{
    protected Group GivenGroup { get; }
    protected override int GivenCount => 10;
    protected override bool GivenLessonIncluded => true;
    protected override string GivenLanguages => "1";
    public override Owner GivenOwner { get; }
    
    public GroupId()
    {
        SystemClock.Override(new DateTime(2022, 2, 20, 12, 0, 0));
        var owner = DataBuilder.SampleUser().Build();
        GivenGroup = DataBuilder.SampleGroup().Build();
        owner.Groups.Add(GivenGroup);

        var card = new Card
        {
            Front = DataBuilder.FrontSide().Build(),
            Back = DataBuilder.BackSide().Build(),
            Details = new List<Detail>
            {
                DataBuilder.Detail().With(x => x.SideType = 1).With(x => x.IsQuestion = true)
                    .With(x => x.NextRepeat = new DateTime(2022, 2, 19)).Build(),
                DataBuilder.Detail().With(x => x.SideType = 2).With(x => x.IsQuestion = false)
                    .With(x => x.NextRepeat = null).Build()
            }
        };
        GivenGroup.Cards.Add(card);
        
        var anotherGroup = DataBuilder.SampleGroup().Build();
        owner.Groups.Add(anotherGroup);

        var anotherCard = new Card
        {
            Front = DataBuilder.FrontSide().Build(),
            Back = DataBuilder.BackSide().Build(),
            Details = new List<Detail>
            {
                DataBuilder.Detail().With(x => x.SideType = 1).With(x => x.IsQuestion = true)
                    .With(x => x.NextRepeat = new DateTime(2022, 2, 19)).Build(),
                DataBuilder.Detail().With(x => x.SideType = 2).With(x => x.IsQuestion = false)
                    .With(x => x.NextRepeat = null).Build()
            }
        };
        anotherGroup.Cards.Add(anotherCard);
        GivenOwner = owner;

        
    }
}

internal class GroupIdDefined : GroupId
{
    protected override string GivenGroupId => GivenGroup.Id.ToString();
    public override IEnumerable<RepeatDto> ExpectedResponse { get; }

    public GroupIdDefined()
    {
        ExpectedResponse = new[]
        {
            new RepeatDto(string.Empty, 1, 2, "FrontValue", "FrontExample", "1", "BackValue", "BackExample", "2")
        };
    }
}

internal class GroupIdUndefined : GroupId
{
    protected override string GivenGroupId => null;
    public override IEnumerable<RepeatDto> ExpectedResponse { get; }

    public GroupIdUndefined()
    {
        ExpectedResponse = new[]
        {
            new RepeatDto(string.Empty, 1, 2, "FrontValue", "FrontExample", "1", "BackValue", "BackExample", "2"),
            new RepeatDto(string.Empty, 1, 2, "FrontValue", "FrontExample", "1", "BackValue", "BackExample", "2")
        };
    }
}