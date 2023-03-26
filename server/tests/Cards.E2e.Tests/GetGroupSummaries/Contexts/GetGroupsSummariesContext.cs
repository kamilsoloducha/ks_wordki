using System.Collections.Generic;
using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetGroupSummaries.Contexts;

internal abstract class GetGroupsSummariesContext
{
    public abstract Owner GivenOwner { get; }
    public abstract IEnumerable<GroupSummaryDto> ExpectedResponse { get; }
}