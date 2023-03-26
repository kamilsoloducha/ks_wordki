using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetGroupSummery.Contexts;

public abstract class GetGroupSummaryContext
{
    public abstract Group GivenGroup { get; }
    public abstract Owner GivenOwner { get; }
    public abstract GroupSummaryDto ExpectedResponse { get; }
}