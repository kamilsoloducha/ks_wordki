using System;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;
using E2e.Tests;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;

namespace Cards.E2e.Tests;

public class CardsTestBase : TestBase
{
    public static Guid UserId = new("12345678-1234-1234-1234-1234567890ab");

    protected CardsTestBase()
    {
    }

    protected Owner Owner = new()
    {
        UserId = UserId
    };

    protected async Task ClearCardsSchema()
    {
        await using var dbContext = new CardsContext();
        await dbContext.Database.ExecuteSqlRawAsync("DELETE from cards.\"Details\"");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE from cards.\"Cards\"");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE from cards.\"Sides\"");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE from cards.\"Groups\"");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE from cards.\"Owners\"");
    }

    protected static Func<EquivalencyAssertionOptions<Side>, EquivalencyAssertionOptions<Side>> SideAssertion =>
        opt => opt.Excluding(x => x.Id)
            .Excluding(x => x.CardBacks)
            .Excluding(x => x.CardFronts);

    protected static Func<EquivalencyAssertionOptions<Detail>, EquivalencyAssertionOptions<Detail>>
        DetailAssertion => opt => opt.Excluding(x => x.Card)
        .Excluding(x => x.CardId);

    protected static Func<EquivalencyAssertionOptions<Group>, EquivalencyAssertionOptions<Group>> GroupAssertion =>
        opt => opt.Excluding(x => x.Id)
            .Excluding(x => x.Cards)
            .Excluding(x => x.Owner)
            .Excluding(x => x.OwnerId);

    protected static Func<EquivalencyAssertionOptions<GroupSummaryDto>,
            EquivalencyAssertionOptions<GroupSummaryDto>>
        GroupSummaryDtoAssertion => config => config.Excluding(x => x.Id);

    protected static Func<EquivalencyAssertionOptions<CardSummaryDto>, EquivalencyAssertionOptions<CardSummaryDto>>
        CardSummaryDtoAssertion => config => config.Excluding(x => x.Id);

    protected static Func<EquivalencyAssertionOptions<SideSummaryDto>, EquivalencyAssertionOptions<SideSummaryDto>>
        SideSummaryDtoAssertion => config => config;

    protected static Func<EquivalencyAssertionOptions<RepeatDto>, EquivalencyAssertionOptions<RepeatDto>>
        RepeatAssertion =>
        config => config.Excluding(x => x.CardId);
}