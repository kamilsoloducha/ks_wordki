using System;
using System.Collections.Generic;
using Api.Model.Requests;
using Cards.Application.Queries.Models;
using Domain.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetDashboardForecast.Contexts
{
    public abstract class GetDashboardForecastContext
    {
        public GetForecast GivenRequest => new(5);
        public abstract IEnumerable<Owner> GivenOwners { get; }
        public abstract IEnumerable<RepeatCount> ExpectedResponse { get; }

        protected GetDashboardForecastContext()
        {
            SystemClock.Override(new DateTime(2022, 2, 20, 13, 30, 5));
        }
    }
}