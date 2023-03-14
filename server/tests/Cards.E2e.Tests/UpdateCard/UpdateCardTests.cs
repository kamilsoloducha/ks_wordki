using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cards.E2e.Tests.UpdateCard.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Cards.E2e.Tests.UpdateCard
{
    [TestFixture(typeof(UpdateCardHappyPath))]
    public class UpdateCardTests<TContext> : CardsTestBase where TContext : UpdateCardContext, new()
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

            Request = new HttpRequestMessage(HttpMethod.Put, $"cards/update/{_context.GivenCard.Id}")
            {
                Content = new StringContent(request, Encoding.UTF8, "application/json")
            };

            await SendRequest();

            Response.Should().BeSuccessful(Response.StatusCode.ToString());

            await using var dbContext = new CardsContext();
            var cards = await dbContext.Cards.Include(x => x.Back).Include(x => x.Front).Include(x => x.Details).ToListAsync();

            cards.Should().HaveCount(1);
            var card = cards.Single();
            card.Front.Should().BeEquivalentTo(_context.ExpectedFront, SideAssertion);
            card.Back.Should().BeEquivalentTo(_context.ExpectedBack, SideAssertion);
            card.Details.Should().BeEquivalentTo(_context.ExpectedDetails, DetailAssertion);
        }
    }
}