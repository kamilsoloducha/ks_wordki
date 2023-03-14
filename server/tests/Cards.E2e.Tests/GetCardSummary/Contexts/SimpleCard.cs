using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.Domain.Enums;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetCardSummary.Contexts
{
    internal class SimpleCard : GetCardSummaryContext
    {
        private Card Card { get; }
        public override string GivenCardId => Card.Id.ToString();
        public override IEnumerable<Owner> GivenOwners { get; }
        public override CardSummaryDto ExpectedResponse { get; }

        public SimpleCard()
        {
            Card = DataBuilder.SampleCard().Build();
            var group = DataBuilder.SampleGroup().Build();
            group.Cards.Add(Card);
            var owner = DataBuilder.SampleUser().Build();
            owner.Groups.Add(group);

            GivenOwners = new[] { owner };

            ExpectedResponse = new CardSummaryDto(
                string.Empty,
                new SideSummaryDto((int)SideType.Front, "FrontValue", "FrontExample", string.Empty, 2, true, true),
                new SideSummaryDto((int)SideType.Back, "BackValue", "BackExample", string.Empty, 2, true, true)
            );
        }
    }
}