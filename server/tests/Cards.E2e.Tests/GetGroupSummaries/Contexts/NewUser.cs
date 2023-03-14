using System.Collections.Generic;
using System.Linq;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetGroupSummaries.Contexts
{
    internal class NewUser : GetGroupsSummariesContext
    {
        public override Owner GivenOwner { get; }
        public override IEnumerable<GroupSummaryDto> ExpectedResponse { get; }

        public NewUser()
        {
            var owner = DataBuilder.SampleUser().Build();
            GivenOwner = owner;

            ExpectedResponse = Enumerable.Empty<GroupSummaryDto>();
        }
    }
}