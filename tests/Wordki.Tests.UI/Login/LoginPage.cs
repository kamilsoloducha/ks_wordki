using OpenQA.Selenium;

namespace Wordki.Tests.UI.Login
{
    class LoginPage : Page
    {
        protected override string Url => "http://localhost:3000/login";
        protected override string Title => "Wordki - Login";

        public LoginPage(IWebDriver driver) : base(driver) { }

        public IWebElement UserNameInput => Driver.FindElement(By.Id("userName"));

        public IWebElement PasswordInput => Driver.FindElement(By.Id("password"));

        public IWebElement Submit => Driver.FindElement(By.CssSelector("input[type=submit]"));
    }
}
