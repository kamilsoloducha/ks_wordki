using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Groups;

[TestFixture]
public class ShowGroupCards : Utils.UITestBase
{
    private readonly GroupsPage _page;

    public ShowGroupCards()
    {
        _page = new GroupsPage(Driver, ClientHost);
    }

    [SetUp]
    public void Setup()
    {
        Server.AddGetEndpoint("/groups/userid", new
        {
            groups = new[]
            {
                new
                {
                    id = "groupId",
                    name = "groupName",
                    front = 1,
                    back = 2,
                    cardsCount = 10,
                    cardsEnabled = 10
                }
            }
        }).AddPostEndpoint("/groups/update", "groupId", x => true);
    }

    void GivenLoginUser() => LoginUser();

    void WhenUserGoToGroupsPage() => _page.NavigateTo();
    void AndWhenPageIsReady() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);
    void AndWhenUserToEditGroup() => _page.Groups.First().Click();

    void ThenGroupDetailsPageShouldAppears() => Driver.Url.Should().Be(ClientHost+"/cards/groupId");

    [Test]
    public void Test() => this.BDDfy();
}