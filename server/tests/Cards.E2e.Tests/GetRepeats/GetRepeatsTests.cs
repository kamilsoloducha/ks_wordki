using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.GetRepeats.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetRepeats;

[TestFixture(typeof(LessonIncludedTrue))]
[TestFixture(typeof(LessonIncludedFalse))]
[TestFixture(typeof(GroupIdDefined))]
[TestFixture(typeof(GroupIdUndefined))]
[TestFixture(typeof(CountEqualThenAvailable))]
[TestFixture(typeof(CountBiggerThenAvailable))]
[TestFixture(typeof(CountLesserThenAvailable))]
public class GetRepeatsTests<TContext> : CardsTestBase where TContext : GetRepeatsContext, new()
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
        Request = new HttpRequestMessage(HttpMethod.Get, $"repeats/{_context.GivenRequest}");

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        var response = await Response.Content.ReadFromJsonAsync<IEnumerable<RepeatDto>>();

        response.Should().BeEquivalentTo(_context.ExpectedResponse, RepeatAssertion);
    }

    
}