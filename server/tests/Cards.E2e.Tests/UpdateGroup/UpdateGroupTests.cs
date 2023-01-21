using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Cards.E2e.Tests.UpdateGroup;

[TestFixture(typeof(UpdateGroupHappyPath))]
public class UpdateGroupTests<TContext> : CardsTestBase where TContext : UpdateGroupContext, new()
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
        var request = JsonConvert.SerializeObject(_context.GivenCommand);

        Request = new HttpRequestMessage(HttpMethod.Put, "groups/update")
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        await SendRequest();

        var content = await Response.Content.ReadAsStringAsync();
        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        await using var dbContext = new CardsContext();
        var group =  await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == UpdateGroupContext.GroupId);

        group.Should().BeEquivalentTo(_context.ExpectedGroup, option =>
            option.Excluding(x => x.Cards)
                .Excluding(x => x.Owner)
                .Excluding(x => x.OwnerId)
        );
    }

}