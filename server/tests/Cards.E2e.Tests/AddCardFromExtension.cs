using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cards.Domain.ValueObjects;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Cards.E2e.Tests;

[TestFixture]
public class AddCardFromExtension : CardsTestBase
{
    [SetUp]
    public async Task Setup()
    {
        await ClearCardsSchema();
        
        await using var dbContext = new CardsContext(GetDbContextOptions<CardsContext>());
        var owner = Owner;
        await dbContext.AddAsync(owner);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task WhenUserAddFirstTime_ShouldCreateGroup()
    {
        // Arrange
        var requestBody = new Api.Model.Requests.AddCardFromExtension
        {
            Value = "new_word"
        };
        Request = new HttpRequestMessage(HttpMethod.Post, "/cards/add/extension")
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
        };

        // Act
        await SendRequest();

        // Assert
        Response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        await using var dbContext = new CardsContext(GetDbContextOptions<CardsContext>());
        var group = await dbContext.Groups
            .Include(x => x.Cards).ThenInclude(card => card.Front)
            .Include(x => x.Cards).ThenInclude(card => card.Back)
            .SingleAsync();
        
        group.Name.Should().Be(GroupName.ChromeExtenstionGroupName.Text);
        group.Cards.Should().ContainSingle();

        var card = group.Cards.First();

        card.Front.Label.Should().Be(requestBody.Value);
        card.Back.Label.Should().Be(requestBody.Value);
    }

    [Test]
    public async Task WhenGroupExists_ShouldUseExistingGroup()
    {
        // Arrange
        await using (var initDbContext = new CardsContext(GetDbContextOptions<CardsContext>()))
        {
            var initGroup = new Group
            {
                Name = GroupName.ChromeExtenstionGroupName.Text,
                Back = "back",
                Front = "front"
            };
            var owner = await initDbContext.Owners.FirstOrDefaultAsync();
            owner.Groups.Add(initGroup);
            await initDbContext.SaveChangesAsync();
        }
        var requestBody = new Api.Model.Requests.AddCardFromExtension
        {
            Value = "new_word"
        };
        Request = new HttpRequestMessage(HttpMethod.Post, "/cards/add/extension")
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
        };

        // Act
        await SendRequest();

        // Assert
        Response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        await using var dbContext = new CardsContext(GetDbContextOptions<CardsContext>());
        var group = await dbContext.Groups
            .Include(x => x.Cards).ThenInclude(card => card.Front)
            .Include(x => x.Cards).ThenInclude(card => card.Back)
            .SingleAsync();
        
        group.Cards.Should().ContainSingle();

        var card = group.Cards.First();

        card.Front.Label.Should().Be(requestBody.Value);
        card.Back.Label.Should().Be(requestBody.Value);
    }
    
    [Test]
    public async Task WhenGroupWithCardExists_ShouldUseExistingGroup()
    {
        // Arrange
        await using (var initDbContext = new CardsContext(GetDbContextOptions<CardsContext>()))
        {
            var initGroup = new Group
            {
                Name = GroupName.ChromeExtenstionGroupName.Text,
                Back = "back",
                Front = "front",
                Cards = [DataBuilder.SampleCard().Build()]
            };
            var owner = await initDbContext.Owners.FirstOrDefaultAsync();
            owner.Groups.Add(initGroup);
            await initDbContext.SaveChangesAsync();
        }
        var requestBody = new Api.Model.Requests.AddCardFromExtension
        {
            Value = "new_word"
        };
        Request = new HttpRequestMessage(HttpMethod.Post, "/cards/add/extension")
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
        };

        // Act
        await SendRequest();

        // Assert
        Response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        await using var dbContext = new CardsContext(GetDbContextOptions<CardsContext>());
        var group = await dbContext.Groups
            .Include(x => x.Cards).ThenInclude(card => card.Front)
            .Include(x => x.Cards).ThenInclude(card => card.Back)
            .SingleAsync();

        group.Cards.Should().HaveCount(2);
    }
}