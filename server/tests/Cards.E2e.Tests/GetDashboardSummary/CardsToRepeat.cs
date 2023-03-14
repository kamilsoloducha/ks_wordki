using System.Collections.Generic;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetDashboardSummary
{
    public class CardsToRepeat : GetDashboardSummaryContext
    {
        public override IEnumerable<Owner> GivenOwners { get; }
        public override Application.Queries.GetDashboardSummary.Response ExpectedResponse { get; }

        public CardsToRepeat()
        {
            var owner = DataBuilder.SampleUser().Build();
            var group = DataBuilder.SampleGroup().Build();
            owner.Groups.Add(group);
            group.Cards.Add(
                DataBuilder.SampleCard().With(x => x.Details = new List<Detail>
                {
                    DataBuilder.FrontDetails().Build(),
                    DataBuilder.BackDetails().With(d => d.IsQuestion = false).With(d => d.NextRepeat = null).Build()
                }).Build()
            );

            GivenOwners = new[] { owner };

            ExpectedResponse = new(1, 2, 1);
        }
    }
}