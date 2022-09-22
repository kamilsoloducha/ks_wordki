using System;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Cards;
using Wordki.Tests.UI.Common;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Groups;

[TestFixture]
public class EditingGroup : Utils.UITestBase
{
    private CardsPage _cardsPage;
    private GroupDialog _groupDialog;
    private GroupSettingsDialog _groupSettingsDialog;

    [SetUp]
    public void Setup()
    {
        _cardsPage = new CardsPage(Driver, ClientHost);
        _groupDialog = new GroupDialog(Driver);
        _groupSettingsDialog = new GroupSettingsDialog(Driver);
        
        Server.AddGetEndpoint($"/cards/userid/{CardsPage.GROUP_ID}", new
            {
                cards = Array.Empty<object>()
            })
            .AddGetEndpoint($"/groups/details/{CardsPage.GROUP_ID}", new
            {
                id = CardsPage.GROUP_ID,
                name = CardsPage.GROUP_NAME,
                front = 1,
                back = 2
            })
            .AddPutEndpoint("/groups/update", new { }, x => true);
    }

    void GivenLoginUser() => LoginUser();

    void WhenUserGoToGroupsPage() => _cardsPage.NavigateTo();
    void AndWhenPageIsReady() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);
    void AndWhenUserOpenOptions() => _cardsPage.GroupSettingsButton.Click();
    void AndWhenUserSeeOptions() => _groupSettingsDialog.WaitFor();
    void AndWhenUserClickEditGroup() => _groupSettingsDialog.EditGroupButton.Click();
    void AndWhenUserSeeGroupEditDialog() => _groupDialog.WaitFor();
    void AndWhenUserFillTheForm() => _groupDialog.FillFormWith("New Group", 2, 1);
    void AndWhenUserSaveGroup() => _groupDialog.SaveAndWait();

    void ThenServerShouldReceiveRequest() =>
        Server.LogEntries.Should()
            .Contain(x => x.RequestMessage.Method == HttpMethod.Put.Method &&
                          x.RequestMessage.Path.Contains("/groups/update"));

    [Test]
    public void Test() => this.BDDfy();
}