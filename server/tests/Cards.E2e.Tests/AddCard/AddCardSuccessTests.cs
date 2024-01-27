using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using Infrastructure.Services.HashIds;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Cards.E2e.Tests.AddCard;

[TestFixture(typeof(NewCardUsedInLesson))]
[TestFixture(typeof(NewCardNotUsedInLesson))]
public class AddCardSuccessTests<TContext> : CardsTestBase where TContext : AddCardSuccessContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearCardsSchema();
        
        await using var dbContext = new CardsContext(GetDbContextOptions<CardsContext>());
        
        var owner = Owner;
        owner.Groups.Add(_context.GivenGroup);
        await dbContext.AddAsync(owner);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Test()
    {
        var request = JsonConvert.SerializeObject(_context.GivenRequest);

        Request = new HttpRequestMessage(HttpMethod.Post, $"cards/add/{_context.GivenGroup.Id}")
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        await SendRequest();
        
        var content = await Response.Content.ReadAsStringAsync();
        Response.Should().BeSuccessful(Response.StatusCode.ToString());
        var response = JsonConvert.DeserializeObject<string>(content);
        var cardId = new TestHashIdsService().GetLongId(response);

        cardId.Should().BeGreaterThan(0);

        await using var dbContext = new CardsContext(GetDbContextOptions<CardsContext>());

        var cards = dbContext.Cards.Include(x => x.Back).Include(x => x.Front).ToList();
        cards.Should().BeEquivalentTo(_context.ExpectedCards, opt =>
            opt.Including(c => c.Back.Example)
                .Including(c => c.Back.Label)
                .Including(c => c.Front.Example)
                .Including(c => c.Front.Label)
        );

        var details = dbContext.Details.ToList();
        details.Should().BeEquivalentTo(_context.ExpectedDetails, opt =>
            opt.Including(c => c.Counter)
                .Including(c => c.Drawer)
                .Including(c => c.IsTicked)
                .Including(c => c.IsQuestion)
                .Including(c => c.NextRepeat)
        );
    }
}