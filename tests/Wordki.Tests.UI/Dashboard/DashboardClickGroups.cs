using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Dashboard;

[TestFixture]
public class DashboardClickGroups : Utils.UITestBase
{
    private static readonly DateTime _today = new(2022, 2, 2);
    private readonly DashboardPage _page;

    public DashboardClickGroups()
    {
        _page = new DashboardPage(Driver, ClientHost);
    }

    [SetUp]
    public void Setup()
    {
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
            )
            .AddGetEndpoint(
                "/groups/userid",
                new { });
    }

    void GivenLoginUser() => SetAuthorizationCookies();

    void WhenUserGoToDashboardPage() => Driver.Navigate().GoToUrl(_page.Address);
    
    void AndPageIsLoaded() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);

    void AndWhenUserClickGroups() => _page.Groups.Click();

    void ThenGroupsInfoIsDisplayed() => Driver.Url.Should().Contain("groups");

    [Test]
    public void ExecuteTest() => this.BDDfy();
    
    
}