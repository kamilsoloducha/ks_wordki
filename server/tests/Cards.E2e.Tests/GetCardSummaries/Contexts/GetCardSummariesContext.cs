using System.Collections.Generic;
using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetCardSummaries.Contexts;

public abstract class GetCardSummariesContext
{
    public abstract string GivenGroupId { get; }
    public abstract IEnumerable<Owner> GivenOwners { get; }
    public abstract IEnumerable<CardSummaryDto> ExpectedResponse { get; }
}