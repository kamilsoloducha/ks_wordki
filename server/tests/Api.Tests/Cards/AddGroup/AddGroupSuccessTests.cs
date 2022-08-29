using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Tests.Model.Cards;
using Cards.Application.Commands;
using FluentAssertions;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using Users.Application.Commands;
using Wordki.Tests.E2E.Feature;

namespace Api.Tests.Cards;

[TestFixture(typeof(SimpleGroup), IgnoreReason = "not ready")]
public class AddGroupSuccessTests<TContext> : CardsTestBase where TContext : AddGroupSuccessContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearCardsSchema();
    }

    [Test]
    public async Task Test()
    {
        var request = JsonConvert.SerializeObject(_context.GivenRequest);

        Request = new HttpRequestMessage(HttpMethod.Post, "groups/add")
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        await SendRequest();

        var responseContent = await Response.Content.ReadAsStringAsync();
        Response.Should().BeSuccessful(Response.StatusCode.ToString());
        var groupId = new TestHashIdsService().GetLongId(responseContent);

        groupId.Should().BeGreaterThan(0);

        await using var dbContext = new CardsContext();
        var group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == groupId);
        group.OwnerId.Should().Be(_context.ExpectedGroup.OwnerId);
        group.Name.Should().Be(_context.ExpectedGroup.Name);
        group.Front.Should().Be(_context.ExpectedGroup.Front);
        group.Back.Should().Be(_context.ExpectedGroup.Back);
    }
}

public abstract class AddGroupSuccessContext
{
    public abstract AddGroup.Command GivenRequest { get; }

    public abstract Group ExpectedGroup { get; }
}

public class SimpleGroup : AddGroupSuccessContext
{
    public override AddGroup.Command GivenRequest { get; } = new()
    {
        UserId = CardsTestBase.OwnerId,
        GroupName = "groupName",
        Front = 1,
        Back = 2
    };

    public override Group ExpectedGroup { get; } = new()
    {
        Back = 2, 
        Front = 1,
        Name = "groupName",
        OwnerId = CardsTestBase.OwnerId,
    };
}