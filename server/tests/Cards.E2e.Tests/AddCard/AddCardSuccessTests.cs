using System.Linq;
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

namespace Cards.E2e.Tests.AddCard;

[TestFixture(typeof(SimpleNewCard))]
[TestFixture(typeof(SimpleNewCardIsUsed))]
public class AddCardSuccessTests<TContext> : CardsTestBase where TContext : AddCardSuccessContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearCardsSchema();
        
        await using var dbContext = new CardsContext();

        _context.GivenGroup.OwnerId = OwnerId;
        
        await dbContext.Groups.AddAsync(_context.GivenGroup);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Test()
    {
        var request = JsonConvert.SerializeObject(_context.GivenRequest);

        Request = new HttpRequestMessage(HttpMethod.Post, "cards/add")
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        await SendRequest();
        
        var content = await Response.Content.ReadAsStringAsync();
        Response.Should().BeSuccessful(Response.StatusCode.ToString());
        var response = JsonConvert.DeserializeObject<ResponseBase<string>>(content);
        var cardId = new TestHashIdsService().GetLongId(response.Response);

        cardId.Should().BeGreaterThan(0);

        await using var dbContext = new CardsContext();

        var cards = dbContext.Cards.Include(x => x.Back).Include(x => x.Front).ToList();
        cards.Should().BeEquivalentTo(_context.ExpectedCards, opt =>
            opt.Including(c => c.IsPrivate)
                .Including(c => c.Back.Example)
                .Including(c => c.Back.Type)
                .Including(c => c.Back.Value)
                .Including(c => c.Front.Example)
                .Including(c => c.Front.Type)
                .Including(c => c.Front.Value)
        );

        var details = dbContext.Details.ToList();
        details.Should().BeEquivalentTo(_context.ExpectedDetails, opt =>
            opt.Including(c => c.Comment)
                .Including(c => c.Counter)
                .Including(c => c.Drawer)
                .Including(c => c.IsTicked)
                .Including(c => c.LessonIncluded)
                .Including(c => c.NextRepeat)
        );
    }
}