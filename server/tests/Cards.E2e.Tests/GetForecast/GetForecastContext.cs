using System.Collections.Generic;
using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetForecast;

public abstract class GetForecastContext
{
    public abstract IEnumerable<Owner> GivenOwners { get; }
    public abstract IEnumerable<RepeatCount> ExpectedResponse { get; }
}