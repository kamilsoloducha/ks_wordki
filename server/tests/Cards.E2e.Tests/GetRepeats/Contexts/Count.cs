using System;
using System.Collections.Generic;
using Cards.E2e.Tests.Utils;
using Domain.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetRepeats.Contexts;

internal abstract class Count : GetRepeatsContext
{
    private Group GivenGroup { get; }
    protected override string GivenGroupId => GivenGroup.Id.ToString();
    protected override bool GivenLessonIncluded => true;
    protected override string GivenLanguages => "1";
    public override Owner GivenOwner { get; }
    
    public Count()
    {
        SystemClock.Override(new DateTime(2022, 2, 20, 12, 0, 0));
        var owner = DataBuilder.SampleUser().Build();
        GivenGroup = DataBuilder.SampleGroup().Build();
        owner.Groups.Add(GivenGroup);
        
        for (var i = 0; i < 10; i++)
        {
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
        }
        
        GivenOwner = owner;
    }
}