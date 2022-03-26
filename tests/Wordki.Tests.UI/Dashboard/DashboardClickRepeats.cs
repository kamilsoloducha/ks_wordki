using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;

namespace Wordki.Tests.UI.Login
{
    [TestFixture]
    public class DashboardClickRepeats : UITestBase
    {
        private readonly DashboardPage _page;
        public DashboardClickRepeats()
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
            _page.Repeats.Click();
        }

        [Test]
        public void ExecuteTest() => this.BDDfy();
    }
}
