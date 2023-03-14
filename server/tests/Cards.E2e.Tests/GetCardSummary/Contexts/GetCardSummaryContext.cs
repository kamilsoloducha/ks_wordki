using System.Collections.Generic;
using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetCardSummary.Contexts
{
    public abstract class GetCardSummaryContext
    {
        public abstract string GivenCardId { get; }
        public abstract IEnumerable<Owner> GivenOwners { get; }
        public abstract CardSummaryDto ExpectedResponse { get; }
    }
}