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
    private readonly LoginPage _page;
    private static readonly DateTime _today = new (2022, 2, 2);

    public FillFormLoginPage()
    {
        _page = new LoginPage(Driver, ClientHost);
    }
    void GivenSetupServer()
    {
        Server.AddPutEndpoint(
            "/users/login",
            new
            {
                responseCode = 0,
                id= "userid",
                token= "string",
                creatingDateTime =  new DateTime(),
                expirationDateTime=  new DateTime()
            },
            b => true
        );
        Server.AddGetEndpoint(
                "/dashboard/summary/userid",
                new { groupsCount = 10, cardsCount = 20, dailyRepeats = 30 })
            .AddGetEndpoint(
                "/dashboard/forecast",
                new object[]
                {
                    new { Count = 0, Date = _today },
                    new { Count = 0, Date = _today.AddDays(1) },
                    new { Count = 0, Date = _today.AddDays(2) },
                    new { Count = 0, Date = _today.AddDays(3) },
                    new { Count = 0, Date = _today.AddDays(4) },
                    new { Count = 0, Date = _today.AddDays(5) },
                    new { Count = 0, Date = _today.AddDays(6) },
                }
            );
    }

    void AndGivenLogoutUser() => LogoutUser();
    void WhenUserNavigateToLoginPage() => _page.NavigateTo();
    void AndWhenUserFillUserNameField() => _page.UserNameInput.InsertIntoInput("testUserName", false);
    void AndWhenUserFillPasswordField() => _page.PasswordInput.InsertIntoInput("testPassword", false);
    void AndWhenUserClickSubmit()
    {
        _page.Submit.Click();
        DefaultDriverWait
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs(DashboardPage.DASHBOARD_TITLE));
    }
    void ThenUserShouldBeRedirectToDashboard() => Assert.AreEqual(DashboardPage.DASHBOARD_TITLE, Driver.Title);
    // void AndThenServerShouldReceivedRequests() => Server.LogEntries.Should().HaveCount(6);

    [Test]
    public void LoginToApplication() => this.BDDfy();
    
    [TearDown]
    public void TearDown()
    {
        IsLogin = true;
    }

}