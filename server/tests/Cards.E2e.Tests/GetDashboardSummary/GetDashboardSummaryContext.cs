using System.Collections.Generic;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetDashboardSummary
{
    public abstract class GetDashboardSummaryContext
    {
        public abstract IEnumerable<Owner> GivenOwners { get; }
        public abstract Application.Queries.GetDashboardSummary.Response ExpectedResponse { get; }
    }
}