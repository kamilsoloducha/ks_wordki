using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetGroupSummaries.Contexts
{
    internal class SingleGroup : GetGroupsSummariesContext
    {
        public override Owner GivenOwner { get; }
        public override IEnumerable<GroupSummaryDto> ExpectedResponse { get; }

        public SingleGroup()
        {
            var owner = DataBuilder.SampleUser().Build();
            owner.Groups.Add(
                DataBuilder.SampleGroup()
                    .With(x => x.Cards = new List<Card>
                    {
                        DataBuilder.SampleCard().Build(),
                        DataBuilder.SampleCard().Build()
                    })
                    .Build());

            GivenOwner = owner;

            ExpectedResponse = new[]
            {
                new GroupSummaryDto(string.Empty, "GroupName", "1", "2", 2)
            };
        }
    }
}