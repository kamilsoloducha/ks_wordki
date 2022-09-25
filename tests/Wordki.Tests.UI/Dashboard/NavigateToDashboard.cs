using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Dashboard;

[TestFixture]
public class NavigateToDashboard : Utils.UITestBase
{
    private static readonly DateTime _today = new (2022, 2, 2);
    private readonly DashboardPage _page;

    public NavigateToDashboard()
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
            );
    }

    void GivenLoginUser() => LoginUser();
    void WhenUserGoToDashboardPage() => Driver.Navigate().GoToUrl(_page.Address);

    void ThenRepeatInfoIsDisplayed()
    {
        DefaultDriverWait
            .Until(driver => driver.FindElements(By.ClassName("loader")).Count == 0);
        _page.Repeats.Text.Should().Contain("30");
        _page.Cards.Text.Should().Contain("20");
        _page.Groups.Text.Should().Contain("10");
    }

    void AndThenServerReceivedSummaryRequest()
    {
        Server.LogEntries.Should().Contain(x => x.RequestMessage.Method == HttpMethod.Get.Method &&
                                                x.RequestMessage.Path == "/dashboard/summary/userid");
    }
    
    void AndThenServerReceivedForecastRequest()
    {
        Server.LogEntries.Should().Contain(x => x.RequestMessage.Method == HttpMethod.Get.Method &&
                                                x.RequestMessage.Path.Contains("/dashboard/forecast"));
    }

    [Test]
    public void ShowDashboard() => this.BDDfy();
}