using System;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Common;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Cards;

[TestFixture]
public sealed class DeleteCards : Utils.UITestBase
{
    private readonly CardsPage _page;
    private readonly CardDialog _cardDialog;

    public DeleteCards()
    {
        _page = new CardsPage(Driver, ClientHost);
        _cardDialog = new CardDialog(Driver);
    }

    [SetUp]
    public void SetUp()
    {
        Server.AddGetEndpoint($"/cards/userid/{CardsPage.GROUP_ID}", new
            {
                cards = new[]
                {
                    new
                    {
                        id = "cardId1",
                        front = new
                        {
                            type = 0, value = "frontValue", example = "frontExample", drawer = 1, isUsed = true,
                            isTicked = false
                        },
                        back = new
                        {
                            type = 1, value = "backValue", example = "backExample", drawer = 2, isUsed = true,
                            isTicked = false
                        }
                    }
                }
            })
            .AddGetEndpoint($"/groups/details/{CardsPage.GROUP_ID}", new
            {
                id = CardsPage.GROUP_ID,
                name = CardsPage.GROUP_NAME,
                front = 1,
                back = 2
            })
            .AddDeleteEndpoint($"/cards/delete/userid/{CardsPage.GROUP_ID}/cardId1");
    }

    void GivenLoginUser() => SetAuthorizationCookies();


    void WhenUserGoToCardsPage() => _page.NavigateTo();
    void AndWhenPageIsReady() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(driver => driver.FindElements(By.ClassName("card-item")).Count != 0);

    void AndWhenUserClickOnCard() => _page.Cards.First().Click();
    void AndWhenUserDeleteCard() => _cardDialog.DeleteAndWait();

    void ThenServerShouldReceiveRequest() =>
        Server.LogEntries.Should()
            .Contain(x => x.RequestMessage.Method == HttpMethod.Delete.Method &&
                          x.RequestMessage.Path.Contains($"/cards/delete/userid/{CardsPage.GROUP_ID}/cardId1"));

    [Test]
    public void Test() => this.BDDfy();
}