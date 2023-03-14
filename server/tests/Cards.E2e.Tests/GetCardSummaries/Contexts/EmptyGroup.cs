using System.Collections.Generic;
using System.Linq;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetCardSummaries.Contexts
{
    internal class EmptyGroup : GetCardSummariesContext
    {
        private Group Group { get; }
        public override string GivenGroupId => Group.Id.ToString();
        public override IEnumerable<Owner> GivenOwners { get; }
        public override IEnumerable<CardSummaryDto> ExpectedResponse { get; }

        public EmptyGroup()
        {
            Group = DataBuilder.SampleGroup().Build();
            var owner = DataBuilder.SampleUser()
                .Build();
            owner.Groups.Add(Group);
            GivenOwners = new[]
            {
                owner
            };

            ExpectedResponse = Enumerable.Empty<CardSummaryDto>();
        }
    }
}