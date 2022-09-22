using OpenQA.Selenium;

namespace Wordki.Tests.UI.Login;

public class LoginPage : Utils.Page
{
    public const string LOGIN_TITLE = "Wordki - Login";
    public const string LOGIN_URL = "/login";
    public LoginPage(IWebDriver driver, string host) : base(driver, LOGIN_TITLE, LOGIN_URL, host)
    {
    }
    
    public IWebElement UserNameInput => Driver.FindElement(By.Id("userName"));
    public IWebElement PasswordInput => Driver.FindElement(By.Id("password"));
    public IWebElement Submit => Driver.FindElement(By.CssSelector("input[type=submit]"));
}

public class LogoutPage : Utils.Page
{
    public const string LOGIN_TITLE = "''";
    public const string LOGIN_URL = "/logout";
    public LogoutPage(IWebDriver driver, string host) : base(driver, LOGIN_TITLE, LOGIN_URL, host)
    {
    }
    
}