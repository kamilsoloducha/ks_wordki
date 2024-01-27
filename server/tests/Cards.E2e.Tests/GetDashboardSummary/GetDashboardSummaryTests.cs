using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Domain.Utils;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetDashboardSummary;

[TestFixture(typeof(NewUser))]
[TestFixture(typeof(DetailsNotIncluded))]
[TestFixture(typeof(CardsToRepeat))]
[TestFixture(typeof(DetailsIncludedNextRepeatExceeded))]
public class GetDashboardSummaryTests<TContext> : CardsTestBase where TContext : GetDashboardSummaryContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        SystemClock.Override(new DateTime(2022, 2, 20));
        await ClearCardsSchema();

        await using var dbContext = new CardsContext(GetDbContextOptions<CardsContext>());
        await dbContext.Owners.AddRangeAsync(_context.GivenOwners);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Test()
    {
        Request = new HttpRequestMessage(HttpMethod.Get, $"dashboard/summary");

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        var response = await Response.Content.ReadFromJsonAsync<Application.Queries.GetDashboardSummary.Response>();

        response.Should().BeEquivalentTo(_context.ExpectedResponse);
    }
}