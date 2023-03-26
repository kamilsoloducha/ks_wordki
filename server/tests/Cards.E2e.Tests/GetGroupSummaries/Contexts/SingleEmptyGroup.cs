using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetGroupSummaries.Contexts;

internal class SingleEmptyGroup : GetGroupsSummariesContext
{
    public override Owner GivenOwner { get; }
    public override IEnumerable<GroupSummaryDto> ExpectedResponse { get; }

    public SingleEmptyGroup()
    {
        var owner = DataBuilder.SampleUser().Build();
        owner.Groups.Add(DataBuilder.SampleGroup().Build());

        GivenOwner = owner;

        ExpectedResponse = new[]
        {
            new GroupSummaryDto(string.Empty, "GroupName", "1", "2", 0)
        };
    }
}