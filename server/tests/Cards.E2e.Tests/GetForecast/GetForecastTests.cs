using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Domain.Utils;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetForecast;

[TestFixture(typeof(NewOwner))]
[TestFixture(typeof(AllDates))]
public class GetForecastTests<TContext> : CardsTestBase where TContext : GetForecastContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        SystemClock.Override(new DateTime(2022, 2, 2));
        await ClearCardsSchema();

        await using var dbContext = new CardsContext();
        await dbContext.Owners.AddRangeAsync(_context.GivenOwners);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Test()
    {
        Request = new HttpRequestMessage(HttpMethod.Get, $"dashboard/forecast?" +
                                                         $"{nameof(Application.Queries.GetForecast.Query.UserId)}={UserId}&" +
                                                         $"{nameof(Application.Queries.GetForecast.Query.StartDate)}=2022-02-02&" +
                                                         $"{nameof(Application.Queries.GetForecast.Query.Count)}=5");

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        var response = await Response.Content.ReadFromJsonAsync<IEnumerable<RepeatCount>>();

        response.Should().BeEquivalentTo(_context.ExpectedResponse);
    }
}