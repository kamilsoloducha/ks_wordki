using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.GetGroupSummery.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetGroupSummery
{
    [TestFixture(typeof(EmptyGroup))]
    [TestFixture(typeof(SimpleGroup))]
    public class GetGroupSummaryTests<TContext> : CardsTestBase where TContext : GetGroupSummaryContext, new()
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
            Request = new HttpRequestMessage(HttpMethod.Get, $"groups/summary/{_context.GivenGroup.Id}");

            await SendRequest();

            Response.Should().BeSuccessful(Response.StatusCode.ToString());

            var response = await Response.Content.ReadFromJsonAsync<GroupSummaryDto>();

            response.Should().BeEquivalentTo(_context.ExpectedResponse, GroupSummaryDtoAssertion);
        }
    }
}