using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;

namespace Wordki.Tests.UI.Login
{
    [TestFixture]
    public class DashboardNewUser : UITestBase
    {
        private static DateTime _today = new DateTime(2022, 2, 2);
        private readonly DashboardPage _page;
        public DashboardNewUser()
        {
            _page = new DashboardPage(Driver);
        }

        void GivenCookies() => SetAuthorizationCookie();

        void AndGivenServerSetup()
            => Server.AddGetEndpoint(
                "/cards/dashboard/summary/userid",
                new { groupsCount = 0, cardsCount = 0, dailyRepeats = 0 }
            ).AddGetEndpoint(
                "/dashboard/forecast",
                new object[] {
                    new { Count = 0, Date = _today },
                    new { Count = 0, Date = _today.AddDays(1) },
                    new { Count = 0, Date = _today.AddDays(2) },
                    new { Count = 0, Date = _today.AddDays(3) },
                    new { Count = 0, Date = _today.AddDays(4) },
                    new { Count = 0, Date = _today.AddDays(5) },
                    new { Count = 0, Date = _today.AddDays(6) },
                }
            );

        void WhenUserNavigateToDashbaord() => _page.NavigateTo();

        void ThenRepeatInfoIsDisplayed()
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("info-container")));
            _page.Repeats.Click();
        }

        [Test]
        public void ExecuteTest() => this.BDDfy();
    }
}
