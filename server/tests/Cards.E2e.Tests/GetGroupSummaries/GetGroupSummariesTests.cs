using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.GetGroupSummaries.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetGroupSummaries;

[TestFixture(typeof(NewUser))]
[TestFixture(typeof(SingleEmptyGroup))]
[TestFixture(typeof(SingleGroup))]
[TestFixture(typeof(MultipleGroups))]
internal class GetGroupSummariesTests<TContext> : CardsTestBase where TContext : GetGroupsSummariesContext, new()
{

    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearCardsSchema();

        await using var dbContext = new CardsContext();

        await dbContext.Owners.AddAsync(_context.GivenOwner);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Test()
    {
        Request = new HttpRequestMessage(HttpMethod.Get, "groups/summaries");

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        var response = await Response.Content.ReadFromJsonAsync<IEnumerable<GroupSummaryDto>>();

        response.Should().BeEquivalentTo(_context.ExpectedResponse, GroupSummaryDtoAssertion);
    }
    
    
}