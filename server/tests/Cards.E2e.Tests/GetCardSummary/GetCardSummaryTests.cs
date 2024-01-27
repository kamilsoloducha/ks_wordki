using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.GetCardSummary.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetCardSummary;

[TestFixture(typeof(SimpleCard))]
public class GetCardSummaryTests<TContext> : CardsTestBase where TContext : GetCardSummaryContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearCardsSchema();

        await using var dbContext = new CardsContext(GetDbContextOptions<CardsContext>());
        await dbContext.Owners.AddRangeAsync(_context.GivenOwners);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Test()
    {
        Request = new HttpRequestMessage(HttpMethod.Get, $"cards/summary/{_context.GivenCardId}");

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        var response = await Response.Content.ReadFromJsonAsync<CardSummaryDto>();

        response.Should().BeEquivalentTo(_context.ExpectedResponse, CardSummaryDtoAssertion);
    }
}