using System.Collections.Generic;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetDashboardSummary;

public class NewUser : GetDashboardSummaryContext
{
    public override IEnumerable<Owner> GivenOwners { get; }
    public override Application.Queries.GetDashboardSummary.Response ExpectedResponse { get; }

    public NewUser()
    {
        var owner = new Owner
        {
            UserId = CardsTestBase.UserId,
            Id = CardsTestBase.OwnerId
        };
        GivenOwners = new[] { owner };

        ExpectedResponse = new Application.Queries.GetDashboardSummary.Response
        {
            CardsCount = 0,
            DailyRepeats = 0,
            GroupsCount = 0,
            RepeatsCounts = null
        };
    }
}