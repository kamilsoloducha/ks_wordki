using System;
using System.Collections.Generic;
using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetForecast;

public class NewOwner : GetForecastContext
{
    public override IEnumerable<Owner> GivenOwners { get; }
    public override IEnumerable<RepeatCount> ExpectedResponse { get; }

    public NewOwner()
    {
        var owner = new Owner
        {
            Id = CardsTestBase.UserId
        };
        GivenOwners = new[] { owner };

        ExpectedResponse = new[]
        {
            new RepeatCount { Count = 0, Date = new DateTime(2022, 2, 2).Date },
            new RepeatCount { Count = 0, Date = new DateTime(2022, 2, 3).Date },
            new RepeatCount { Count = 0, Date = new DateTime(2022, 2, 4).Date },
            new RepeatCount { Count = 0, Date = new DateTime(2022, 2, 5).Date },
            new RepeatCount { Count = 0, Date = new DateTime(2022, 2, 6).Date },
        };
    }
}