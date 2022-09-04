using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Requests;
using Cards.E2e.Tests.Models.Cards;
using FluentAssertions;
using Infrastructure.Services;
using Infrastructure.Services.HashIds;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Cards.E2e.Tests.AddGroup;

[TestFixture(typeof(SimpleGroup))]
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

        var content = await Response.Content.ReadAsStringAsync();
        Response.Should().BeSuccessful(Response.StatusCode.ToString());
        var response = JsonConvert.DeserializeObject<ResponseBase<string>>(content);
        var groupId = new TestHashIdsService().GetLongId(response.Response);

        groupId.Should().BeGreaterThan(0);

        await using var dbContext = new CardsContext();
        var group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == groupId);
        group.OwnerId.Should().Be(_context.ExpectedGroup.OwnerId);
        group.Name.Should().Be(_context.ExpectedGroup.Name);
        group.Front.Should().Be(_context.ExpectedGroup.Front);
        group.Back.Should().Be(_context.ExpectedGroup.Back);
    }
}