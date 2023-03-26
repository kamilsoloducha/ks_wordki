using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.GetCardSummaries.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetCardSummaries;

[TestFixture(typeof(EmptyGroup))]
[TestFixture(typeof(GroupWithCard))]
public class GetCardSummariesTests<TContext> : CardsTestBase where TContext : GetCardSummariesContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearCardsSchema();

        await using var dbContext = new CardsContext();
        await dbContext.Owners.AddRangeAsync(_context.GivenOwners);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Test()
    {
        Request = new HttpRequestMessage(HttpMethod.Get, $"cards/summaries/{_context.GivenGroupId}");

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        var response = await Response.Content.ReadFromJsonAsync<IEnumerable<CardSummaryDto>>();

        response.Should().BeEquivalentTo(_context.ExpectedResponse, CardSummaryDtoAssertion);
    }
}