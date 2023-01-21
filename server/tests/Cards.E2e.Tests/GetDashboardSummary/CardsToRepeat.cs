using System;
using System.Collections.Generic;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetDashboardSummary;

public class CardsToRepeat : GetDashboardSummaryContext
{
    public override IEnumerable<Owner> GivenOwners { get; }
    public override Application.Queries.GetDashboardSummary.Response ExpectedResponse { get; }

    public CardsToRepeat()
    {
        var owner = DataBuilder.EmptyOwner().Build();
        var group = DataBuilder.EmptyGroup().Build();
        owner.Groups.Add(group);

        var card = new Card
        {
            Front = DataBuilder.FrontSide().With(x => x.Id = 1).Build(),
            Back = DataBuilder.BackSide().With(x => x.Id = 2).Build(),
            Id = 1,
            FrontId = 1,
            BackId = 2,
            IsPrivate = true
        };
        group.Cards.Add(card);

        owner.Details.Add(
            DataBuilder.Detail().With(x => x.Id = 1).With(x => x.SideId = 1).With(x => x.OwnerId = owner.Id)
                .With(x => x.LessonIncluded = true)
                .With(x => x.NextRepeat = new DateTime(2022, 1, 1))
                .Build());

        owner.Details.Add(
            DataBuilder.Detail().With(x => x.Id = 2).With(x => x.SideId = 2).With(x => x.OwnerId = owner.Id)
                .With(x => x.LessonIncluded = false)
                .With(x => x.NextRepeat = new DateTime(2022, 1, 1))
                .Build());

        GivenOwners = new[] { owner };

        ExpectedResponse = new Application.Queries.GetDashboardSummary.Response
        {
            CardsCount = 2,
            DailyRepeats = 1,
            GroupsCount = 1,
            RepeatsCounts = null
        };
    }
}