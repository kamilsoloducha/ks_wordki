using System.Collections.Generic;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.TickCard.Contexts;

internal class TickCard : TickCardContext
{
    private Card GivenCard { get; }
    public override Owner GivenOwner { get; }
    public override string GivenCardId => GivenCard.Id.ToString();
    public override IEnumerable<Detail> ExpectedDetails { get; }

    public TickCard()
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
                DataBuilder.Detail().With(x => x.SideType = 1).With(x => x.IsTicked = false).Build(),
                DataBuilder.Detail().With(x => x.SideType = 2).With(x => x.IsTicked = false).Build()
            }
        };
        group.Cards.Add(GivenCard);
        GivenOwner = owner;

        ExpectedDetails = new[]
        {
            DataBuilder.Detail().With(x => x.SideType = 1).With(x => x.IsTicked = true).Build(),
            DataBuilder.Detail().With(x => x.SideType = 2).With(x => x.IsTicked = true).Build()
        };
    }
}