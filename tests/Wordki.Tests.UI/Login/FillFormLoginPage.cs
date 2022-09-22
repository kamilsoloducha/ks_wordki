using NUnit.Framework;
using TestStack.BDDfy;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;
using System;
using Wordki.Tests.UI.Dashboard;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Login;

[TestFixture]
public class FillFormLoginPage : Utils.UITestBase
{
    private  LoginPage _page;

    void GivenSetupServer()
    {
        _page = new LoginPage(Driver, ClientHost);
        Server.AddPutEndpoint(
            "/users/login",
            new
            {
                responseCode = 0,
                id= "string",
                token= "string",
                creatingDateTime =  new DateTime(),
                expirationDateTime=  new DateTime()
            },
            b => true
        );
    }

    void AndGivenLogoutUser() => LogoutUser();
    void WhenUserNavigateToLoginPage() => _page.NavigateTo();
    void AndWhenUserFillUserNameField() => _page.UserNameInput.InsertIntoInput("testUserName", false);
    void AndWhenUserFillPasswordField() => _page.PasswordInput.InsertIntoInput("testPassword", false);
    void AndWhenUserClickSubmit()
    {
        _page.Submit.Click();
        new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs(DashboardPage.DASHBOARD_TITLE));
    }
    void ThenUserShouldBeRedirectToDashboard() => Assert.AreEqual(DashboardPage.DASHBOARD_TITLE, Driver.Title);
    // void AndThenServerShouldReceivedRequests() => Server.LogEntries.Should().HaveCount(6);

    [Test]
    public void ExecuteTest() => this.BDDfy();

}