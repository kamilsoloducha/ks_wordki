using System.Net.Http;
using System.Threading.Tasks;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Cards.E2e.Tests.DeleteCard;

[TestFixture(typeof(SimpleHappyPath))]
[TestFixture(typeof(DeleteNotExistedCard))]
public class DeleteCardTests<TContext> : CardsTestBase where TContext : DeleteCardContext, new()
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
        Request = new HttpRequestMessage(HttpMethod.Delete,
            $"cards/{_context.GivenCardId}");

        await SendRequest();

        await using var dbContext = new CardsContext();

        (await dbContext.Sides.CountAsync()).Should().Be(_context.ExpectedSideCount);
        (await dbContext.Cards.CountAsync()).Should().Be(_context.ExpectedCardsCount);
        (await dbContext.Groups.CountAsync()).Should().Be(_context.ExpectedGroupsCount);
        (await dbContext.Details.CountAsync()).Should().Be(_context.ExpectedDetailsCount);
    }
}