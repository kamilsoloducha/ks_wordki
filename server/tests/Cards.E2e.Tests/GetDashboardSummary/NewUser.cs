using System.Collections.Generic;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetDashboardSummary
{
    public class NewUser : GetDashboardSummaryContext
    {
        public override IEnumerable<Owner> GivenOwners { get; }
        public override Application.Queries.GetDashboardSummary.Response ExpectedResponse { get; }

        public NewUser()
        {
            var owner = DataBuilder.SampleUser().Build();
            GivenOwners = new[] { owner };

            ExpectedResponse = new(0, 0, 0);
        }
    }
}