using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;

namespace Wordki.Tests.UI.Login;

[TestFixture(Ignore = "not ready")]
public class NavigateToDashboard : UITestBase
{
    private readonly DashboardPage _page;
    public NavigateToDashboard()
    {
        _page = new DashboardPage(Driver);
    }

    void GivenCookies() => SetAuthorizationCookie();

    void AndGivenServerSetup() => Server.AddGetEndpoint(
        "/cards/dashboard/summary/userid",
        new { groupsCount = 10, cardsCount = 20, dailyRepeats = 30 });

    void WhenUserNavigateToDashbaord() => _page.NavigateTo();

    void ThenRepeatInfoIsDisplayed()
    {
        new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("info-container")));
        _page.Repeats.Text.Should().Contain("30");
        _page.Cards.Text.Should().Contain("20");
        _page.Groups.Text.Should().Contain("10");
    }

    [Test]
    public void ExecuteTest() => this.BDDfy();
}