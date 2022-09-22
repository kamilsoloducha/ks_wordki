using OpenQA.Selenium;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Error;

public class ErrorPage : Page
{
    public const string ERROR_TITLE = "Error";
    public const string ERROR_URL = "/error";
    
    public ErrorPage(IWebDriver driver, string host) : base(driver, ERROR_TITLE, ERROR_URL, host)
    {
    }

    public IWebElement Header => Driver.FindElement(By.TagName("h2"));
}