using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Groups;

[TestFixture]
public class DisplayingGroups : Utils.UITestBase
{
    private readonly GroupsPage _page;

    public DisplayingGroups()
    {
        _page = new GroupsPage(Driver, ClientHost);
    }

    [SetUp]
    public void Setup()
    {
        Server.AddGetEndpoint("/groups/userid", new
            {
                groups = new object[]
                {
                    new { id = 1, name = "groupName1", front = 1, back = 2, cardsCount = 2 }
                }
            });
    }

    void GivenLoginUser() => LoginUser();
    
    
    void WhenUserGoToGroupsPage() => Driver.Navigate().GoToUrl(_page.Address);
    void AndWhenPageIsReady() => DefaultDriverWait
        .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);
    
    
    void ThenTitleShouldBeCorrect() => Driver.Title.Should().Be(GroupsPage.GROUPS_TITLE);
    void AndThenGroupsShouldBeVisible() => _page.Groups.Should().HaveCount(1);
    void AndThenServerShouldHandlesRequest() => Server.LogEntries.Should()
        .Contain(x => x.RequestMessage.Method == HttpMethod.Get.Method &&
                      x.RequestMessage.Path.Contains("/groups/userid"));

    [Test]
    public void ShowGroups() =>this.BDDfy(); 
}
