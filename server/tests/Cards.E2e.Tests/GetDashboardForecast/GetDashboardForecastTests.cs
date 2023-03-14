using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.GetDashboardForecast.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetDashboardForecast
{
    [TestFixture(typeof(NewUser))]
    [TestFixture(typeof(AllIncluded))]
    [TestFixture(typeof(AllExcluded))]
    [TestFixture(typeof(QuestionMixed))]
    public class GetDashboardForecastTests<TContext> : CardsTestBase where TContext : GetDashboardForecastContext, new()
    {
        private readonly TContext _context = new();

        [SetUp]
        public async Task Setup()
        {
            await ClearCardsSchema();

            await using var dbContext = new CardsContext();
            await dbContext.Owners.AddRangeAsync(_context.GivenOwners);
            await dbContext.SaveChangesAsync();
        }

        [Test]
        public async Task Test()
        {
            Request = new HttpRequestMessage(HttpMethod.Get,
                $"dashboard/forecast?{nameof(_context.GivenRequest.Count)}={_context.GivenRequest.Count}");

            await SendRequest();

            Response.Should().BeSuccessful(Response.StatusCode.ToString());

            var response = await Response.Content.ReadFromJsonAsync<IEnumerable<RepeatCount>>();

            response.Should().BeEquivalentTo(_context.ExpectedResponse);
        }
    }
}