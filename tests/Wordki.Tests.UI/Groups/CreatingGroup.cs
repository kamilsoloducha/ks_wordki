using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Common;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Groups;

[TestFixture]
public class CreatingGroup : Utils.UITestBase
{
    private readonly GroupsPage _page;
    private readonly GroupDialog _groupDialog;

    public CreatingGroup()
    {
        _page = new GroupsPage(Driver, ClientHost);
        _groupDialog = new GroupDialog(Driver);
    }

    [SetUp]
    public void Setup()
    {
        Server.AddGetEndpoint("/groups/userid", new
        {
            groups = Array.Empty<object>()
        }).AddPostEndpoint("/groups/add", "groupId", x => true);
    }

    void GivenLoginUser() => SetAuthorizationCookies();


    void WhenUserGoToGroupsPage() => Driver.Navigate().GoToUrl(_page.Address);

    void AndWhenPageIsReady() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);

    void AndWhenUserClickCreateGroupButton() => _page.CreateNewGroupButton.Click();
    void AndWhenUserFillTheForm() => _groupDialog.FillFormWith("New Group", 1, 2);
    void AndWhenUserSaveGroup() => _groupDialog.SaveAndWait();

    void ThenServerShouldReceiveRequest() =>
        Server.LogEntries.Should()
            .Contain(x => x.RequestMessage.Method == HttpMethod.Post.Method &&
                          x.RequestMessage.Path.Contains("/groups/add"));

    [Test]
    public void Test() => this.BDDfy();
}