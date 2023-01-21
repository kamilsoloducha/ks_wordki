using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetGroupSummaries;

[TestFixture(typeof(NewUser))]
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
        Request = new HttpRequestMessage(HttpMethod.Get, $"groups/{UserId}");

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        var response = await Response.Content.ReadFromJsonAsync<GetGroupsSummary.Response>();

        response.Should().BeEquivalentTo(_context.ExpectedResponse);
    }
}

internal abstract class GetGroupsSummariesContext
{
    public abstract Owner GivenOwner { get; }
    public abstract GetGroupsSummary.Response ExpectedResponse { get; }
}

internal class NewUser : GetGroupsSummariesContext
{
    public override Owner GivenOwner { get; }
    public override GetGroupsSummary.Response ExpectedResponse { get; }

    public NewUser()
    {
        var owner = DataBuilder.EmptyOwner().Build();
        GivenOwner = owner;

        ExpectedResponse = new GetGroupsSummary.Response()
        {
            Groups = Enumerable.Empty<GetGroupsSummary.GroupSummaryDto>()
        };
    }
}