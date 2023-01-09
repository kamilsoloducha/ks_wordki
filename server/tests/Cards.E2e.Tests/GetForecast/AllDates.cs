using System;
using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetForecast;

public class AllDates : GetForecastContext
{
    public override IEnumerable<Owner> GivenOwners { get; }
    public override IEnumerable<RepeatCount> ExpectedResponse { get; }

    public AllDates()
    {
        var owner = new Owner
        {
            UserId = CardsTestBase.UserId,
            Id = CardsTestBase.OwnerId
        };
        owner.Details.Add(
            DataBuilder.Detail().With(x => x.SideId = 1).With(x => x.OwnerId = owner.Id)
                .With(x => x.NextRepeat = new DateTime(2022, 2, 1))
                .Build());
        owner.Details.Add(
            DataBuilder.Detail().With(x => x.SideId = 2).With(x => x.OwnerId = owner.Id)
                .With(x => x.NextRepeat = new DateTime(2022, 2, 2))
                .Build());
        owner.Details.Add(
            DataBuilder.Detail().With(x => x.SideId = 3).With(x => x.OwnerId = owner.Id)
                .With(x => x.NextRepeat = new DateTime(2022, 2, 3))
                .Build());
        owner.Details.Add(
            DataBuilder.Detail().With(x => x.SideId = 4).With(x => x.OwnerId = owner.Id)
                .With(x => x.NextRepeat = new DateTime(2022, 2, 4))
                .Build());
        owner.Details.Add(
            DataBuilder.Detail().With(x => x.SideId = 5).With(x => x.OwnerId = owner.Id)
                .With(x => x.NextRepeat = new DateTime(2022, 2, 5))
                .Build());
        owner.Details.Add(
            DataBuilder.Detail().With(x => x.SideId = 6).With(x => x.OwnerId = owner.Id)
                .With(x => x.NextRepeat = new DateTime(2022, 2, 6))
                .Build());
        owner.Details.Add(
            DataBuilder.Detail().With(x => x.SideId = 7).With(x => x.OwnerId = owner.Id)
                .With(x => x.NextRepeat = new DateTime(2022, 2, 6))
                .Build());
        owner.Details.Add(
            DataBuilder.Detail().With(x => x.SideId = 8).With(x => x.OwnerId = owner.Id)
                .With(x => x.NextRepeat = new DateTime(2022, 2, 7))
                .Build());
        GivenOwners = new[] { owner };

        ExpectedResponse = new[]
        {
            new RepeatCount { Count = 1, Date = new DateTime(2022, 2, 2).Date },
            new RepeatCount { Count = 1, Date = new DateTime(2022, 2, 3).Date },
            new RepeatCount { Count = 1, Date = new DateTime(2022, 2, 4).Date },
            new RepeatCount { Count = 1, Date = new DateTime(2022, 2, 5).Date },
            new RepeatCount { Count = 2, Date = new DateTime(2022, 2, 6).Date },
        };
    }
}