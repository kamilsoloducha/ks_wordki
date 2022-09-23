using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Cards;

[TestFixture]
public sealed class DisplayingCards : Utils.UITestBase
{
    private readonly CardsPage _page;

    public DisplayingCards()
    {
        _page = new CardsPage(Driver, ClientHost);
    }

    [SetUp]
    public void SetUp()
    {
        Server.AddGetEndpoint($"/cards/userid/{CardsPage.GROUP_ID}", new
            {
                cards = new []
                {
                    new
                    {
                        id = "cardId1",
                        front = new { type= 0, value="frontValue", example="frontExample", drawer = 1, isUsed = true, isTicked=false },
                        back = new { type= 1, value="backValue", example="backExample", drawer = 2, isUsed = true, isTicked=false }
                    },
                    new
                    {
                        id = "cardId2",
                        front = new { type= 0, value="frontValue", example="frontExample", drawer = 1, isUsed = true, isTicked=false },
                        back = new { type= 1, value="backValue", example="backExample", drawer = 2, isUsed = true, isTicked=false }
                    },
                }
            })
            .AddGetEndpoint($"/groups/details/{CardsPage.GROUP_ID}", new
            {
                id = CardsPage.GROUP_ID,
                name = CardsPage.GROUP_NAME,
                front = 1,
                back = 2
            });
    }
    
    void GivenLoginUser() => LoginUser();


    void WhenUserGoToGroupsPage() => Driver.Navigate().GoToUrl(_page.Address);
    void AndWhenPageIsReady() => DefaultDriverWait
        .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);

    void ThenCardsShouldBeVisible() => _page.Cards.Should().HaveCount(2);
    void AndThenServerShouldHandleCardsRequest() => Server.LogEntries.Should()
        .Contain(x => x.RequestMessage.Method == HttpMethod.Get.Method &&
                      x.RequestMessage.Path.Contains($"/cards/userid/{CardsPage.GROUP_ID}"));
    
    void AndThenServerShouldHandleGroupDetailsRequest() => Server.LogEntries.Should()
        .Contain(x => x.RequestMessage.Method == HttpMethod.Get.Method &&
                      x.RequestMessage.Path.Contains($"/groups/details/{CardsPage.GROUP_ID}"));

    [Test]
    public void Test() => this.BDDfy();
}