using System;
using System.Collections.Generic;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetDashboardSummary
{
    public class DetailsIncludedNextRepeatExceeded : GetDashboardSummaryContext
    {
        public override IEnumerable<Owner> GivenOwners { get; }
        public override Application.Queries.GetDashboardSummary.Response ExpectedResponse { get; }

        public DetailsIncludedNextRepeatExceeded()
        {
            var owner = DataBuilder.SampleUser().Build();
            var group = DataBuilder.SampleGroup().Build();
            owner.Groups.Add(group);
            group.Cards.Add(
                DataBuilder.SampleCard().With(x => x.Details = new List<Detail>
                {
                    DataBuilder.FrontDetails().With(d => d.NextRepeat = DateTime.MaxValue).Build(),
                    DataBuilder.BackDetails().Build()
                }).Build()
            );

            GivenOwners = new[] { owner };

            ExpectedResponse = new(1, 2, 1);
        }
    }
}