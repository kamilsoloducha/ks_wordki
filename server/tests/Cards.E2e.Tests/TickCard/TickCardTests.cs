using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cards.E2e.Tests.TickCard.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Cards.E2e.Tests.TickCard;

[TestFixture(typeof(Contexts.TickCard))]
public class TickCardTests<TContext> : CardsTestBase where TContext : TickCardContext, new() 
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
        Request = new HttpRequestMessage(HttpMethod.Put, $"cards/tick/{_context.GivenCardId}")
        {
            Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
        };

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        await using var dbContext = new CardsContext();
        var details = await dbContext.Details.ToListAsync();

        details.Should().BeEquivalentTo(_context.ExpectedDetails, DetailAssertion);

    }
}