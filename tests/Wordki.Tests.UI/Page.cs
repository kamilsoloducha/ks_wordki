using OpenQA.Selenium;
using FluentAssertions;

namespace Wordki.Tests.UI
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

        public void InsertIntoInput(IWebElement element, string text, bool append)
        {
            if (!append) element.Clear();
            element.SendKeys(text);
        }
    }
}
