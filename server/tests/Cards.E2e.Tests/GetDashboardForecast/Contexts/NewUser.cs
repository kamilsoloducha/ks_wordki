using System;
using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetDashboardForecast.Contexts
{
    internal class NewUser : GetDashboardForecastContext
    {
        public override IEnumerable<Owner> GivenOwners { get; }
        public override IEnumerable<RepeatCount> ExpectedResponse { get; }

        public NewUser()
        {
            GivenOwners = new[] { DataBuilder.SampleUser().Build() };

            ExpectedResponse = new List<RepeatCount>
            {
                new(0, new DateTime(2022, 2, 21)),
                new(0, new DateTime(2022, 2, 22)),
                new(0, new DateTime(2022, 2, 23)),
                new(0, new DateTime(2022, 2, 24)),
                new(0, new DateTime(2022, 2, 25))
            };
        }
    }
}