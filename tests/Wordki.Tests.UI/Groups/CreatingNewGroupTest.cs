using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TestStack.BDDfy;
using Wordki.Tests.UI.GroupDetails;
using Wordki.Tests.UI.GroupDialog;

namespace Wordki.Tests.UI.Groups;

[TestFixture(Ignore = "not ready")]
public class NavigateToGroupDetails : UITestBase
{
    protected string GroupsPath = "/groups/userid";
    protected string CardsPath = "/cards/userid/1";
    protected string GroupDetailsPath = "/groups/details/1";
    private readonly GroupsPage _groupPage;
    private readonly GroupDetailsPage _groupDetailsPage;

    public NavigateToGroupDetails()
    {
        _groupPage = new GroupsPage(Driver);
        _groupDetailsPage = new GroupDetailsPage(Driver);
    }

    void GivenCookies() => SetAuthorizationCookie();

    void AndGivenServerSetup() => Server
        .AddGetEndpoint(GroupsPath, new
        {
            groups = new object[]
            {
                new { id = 1, name = "groupName1", front = 1, back = 2, cardsCount = 2 }
            }
        })
        .AddGetEndpoint(CardsPath, new { cards = new object[0] })
        .AddGetEndpoint(GroupDetailsPath, new { id = 1, name = "", front = 1, back = 2 });

    void WhenUserNavigateToGroups() => _groupPage.NavigateTo();
    void AndWhenPageIsLoaded() => _groupPage.WaitForInitialLoad();
    void AndWhenGroupIsClicked() => _groupPage.Groups.First().Click();
    void AndWhenGroupDetailsLoaded() => _groupDetailsPage.WaitForInitialLoad();

    [Test]
    public void Test()
    {
        this.BDDfy();
    }
}

[TestFixture(Ignore = "not ready")]
public class CreatingNewGroupTest : UITestBase
{
    protected string GroupsPath = "/groups/userid";
    protected string AddGroupPath = "/groups/add";
    private readonly GroupsPage _page;
    private readonly GroupDialogPage _dialog;

    public CreatingNewGroupTest()
    {
        _page = new GroupsPage(Driver);
        _dialog = new GroupDialogPage(Driver);
    }

    void GivenCookies() => SetAuthorizationCookie();

    void AndGivenServerSetup() => Server
        .AddGetEndpoint(GroupsPath, new { groups = new object[0] })
        .AddPostEndpoint(AddGroupPath, new ApiResponse<int>()
        {
            Error = string.Empty,
            isCorrect = true,
            Response = 1
        }, x => true);

    void WhenUserNavigateToGroups() => _page.NavigateTo();
    void AndWhenPageIsLoaded() => _page.WaitForInitialLoad();
    void AndWhenCreateNewGroupIsClicked() => _page.CreateNewGroupButton.Click();
    void AndWhenNameIsSet() => _dialog.GroupName.SendKeys("GroupName");
    void AndWhenFrontIsSet() => _dialog.SelectFront(1);
    void AndWhenBackIsSet() => _dialog.SelectFront(1);
    void AndWhenSaveIsClicked() => _dialog.SaveButton.Click();
    void AndWhenProcessIsFinished() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("p-dialog")));

    void ThenServerShouldReceivedRequest()
    {
        var addGroupRequest = Server.LogEntries.FirstOrDefault(x => x.RequestMessage.Path == AddGroupPath);
        addGroupRequest.Should().NotBeNull();
    }

    [Test]
    public void Test() => this.BDDfy();
}

public class ApiResponse<TBody>
{
    public string Error { get; set; }
    public bool isCorrect { get; set; }
    public TBody Response { get; set; }
}