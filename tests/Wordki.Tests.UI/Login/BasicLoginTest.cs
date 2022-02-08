using NUnit.Framework;
using System.Threading;
using TestStack.BDDfy;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;
using System;

namespace Wordki.Tests.UI.Login
{
    abstract class Page
    {
        protected IWebDriver Driver { get; }
        protected virtual string Url { get; }
        protected virtual string Title { get; }

        protected Page(IWebDriver driver)
        {
            Driver = driver;
        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(Url);
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded()
        {

            Driver.Url.Should().Be(Url);
            Driver.Title.Should().Be(Title);
        }
    }

    class LoginPage : Page
    {
        protected override string Url => "http://localhost:3000/login";
        protected override string Title => "Wordki - Login";

        public LoginPage(IWebDriver driver) : base(driver) { }

        public IWebElement UserNameInput => Driver.FindElement(By.Id("userName"));

        public IWebElement PasswordInput => Driver.FindElement(By.Id("password"));

        public IWebElement Submit => Driver.FindElement(By.CssSelector("input[type=submit]"));

        public void InsertIntoInput(IWebElement element, string text, bool append)
        {
            if (!append) element.Clear();
            element.SendKeys(text);
        }
    }


    [TestFixture]
    public class BasicLoginTest : UITestBase
    {
        private readonly LoginPage _page;

        public BasicLoginTest()
        {
            _page = new LoginPage(Driver);
        }

        void WhenUserNavigateToLoginPage()
        {
            Driver.Navigate().GoToUrl($"{AppUrl}/login");
            Thread.Sleep(200);
        }

        void ThenTitleShouldBeSet()
        {
            Assert.AreEqual("Wordki - Login", Driver.Title);
        }

        [Test]
        public void ExecuteTest()
        {
            this.BDDfy();
        }
    }

    public class FillFormLoginPage : UITestBase
    {
        private readonly LoginPage _page;

        public FillFormLoginPage()
        {
            _page = new LoginPage(Driver);
        }
        void GivenSetupServer()
        {
            Server.AddPutEndpoint("/users/login", new { Error = "", IsCorrect = true, Response = new { Token = "token", Id = "guid" } }, b =>
             {
                 return true;
             });
            Console.WriteLine(Server.IsStarted);
        }
        void WhenUserNavigateToLoginPage() => _page.NavigateTo();
        void AndWhenUserFillUserNameField() => _page.InsertIntoInput(_page.UserNameInput, "testUserName", false);
        void AndWhenUserFillPasswordField() => _page.InsertIntoInput(_page.PasswordInput, "testPassword", false);
        void AndWhenUserClickSubmit()
        {
            _page.Submit.Click();
            new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs("Wordki - Dashboard"));
        }
        void ThenUserShouldBeRedirectToDashboard() => Assert.AreEqual("Wordki - Dashboard", Driver.Title);
        void AndThenServerShouldRecivedRequests() => Server.LogEntries.Should().HaveCount(3);

        [Test]
        public void ExecuteTest() => this.BDDfy();

    }


}
