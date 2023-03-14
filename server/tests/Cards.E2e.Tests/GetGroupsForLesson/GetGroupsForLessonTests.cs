using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.GetGroupsForLesson.Contexts;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using FluentAssertions.Equivalency;
using NUnit.Framework;

namespace Cards.E2e.Tests.GetGroupsForLesson
{
    [TestFixture(typeof(NewUser))]
    [TestFixture(typeof(AllAlreadyIncluded))]
    [TestFixture(typeof(AllExcluded))]
    public class GetGroupsForLessonTests<TContext> : CardsTestBase where TContext : GetGroupsForLessonContext, new()
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
                "/groups/forlesson");

            await SendRequest();

            Response.Should().BeSuccessful(Response.StatusCode.ToString());

            var response = await Response.Content.ReadFromJsonAsync<IEnumerable<GroupToLessonDto>>();

            response.Should().BeEquivalentTo(_context.ExpectedResponse, Comparer);
        }

        private Func<EquivalencyAssertionOptions<GroupToLessonDto>, EquivalencyAssertionOptions<GroupToLessonDto>>
            Comparer => config => config.Excluding(x => x.Id);

    }
}