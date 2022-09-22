using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Dashboard;

[TestFixture]
public class DashboardClickRepeats : Utils.UITestBase
{
    private static readonly DateTime _today = new(2022, 2, 2);
    private DashboardPage _page;

    [SetUp]
    public void Setup()
    {
        _page = new DashboardPage(Driver, ClientHost);

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

    void GivenLoginUser() => LoginUser();

    void WhenUserGoToDashboardPage() => _page.NavigateTo();

    void AndWhenPageIsLoaded() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);

    void AndWhenUserClickRepeats() => _page.Repeats.Click();

    void ThenRepeatInfoIsDisplayed()
    {
        Driver.Url.Should().Contain("lesson-settings");
    }

    [Test]
    public void ExecuteTest() => this.BDDfy();
}