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

namespace Wordki.Tests.UI.Groups;

[TestFixture(Ignore = "Not ready")]
public class EditingGroup : Utils.UITestBase
{
    private readonly GroupsPage _page;
    private readonly GroupDialog _groupDialog;

    public EditingGroup()
    {
        _page = new GroupsPage(Driver, ClientHost);
        _groupDialog = new GroupDialog(Driver);
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

    void GivenLoginUser() => SetAuthorizationCookies();


    void WhenUserGoToGroupsPage() => Driver.Navigate().GoToUrl(_page.Address);

    void AndWhenPageIsReady() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);

    void AndWhenUserToEditGroup() => _page.Groups.First().Click();
    void AndWhenUserFillTheForm() => _groupDialog.FillFormWith("New Group", 2, 1);
    void AndWhenUserSaveGroup() => _groupDialog.SaveAndWait();

    void ThenServerShouldReceiveRequest() =>
        Server.LogEntries.Should()
            .Contain(x => x.RequestMessage.Method == HttpMethod.Put.Method &&
                          x.RequestMessage.Path.Contains("/groups/update"));

    [Test]
    public void Test() => this.BDDfy();
}