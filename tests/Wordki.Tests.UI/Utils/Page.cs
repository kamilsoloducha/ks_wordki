using System;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Wordki.Tests.UI.Utils;

public abstract class Page
{
    protected IWebDriver Driver { get; }
    public string Url { get;  }
    public string Host { get; }
    public string Address => Host + Url;

    protected string Title { get; }

    protected Page(IWebDriver driver, string title, string url, string host)
    {
        Driver = driver;
        Title = title;
        Url = url;
        Host = host;
    }

    public void NavigateTo()
    {
        Driver.Navigate().GoToUrl(Address);
        EnsurePageLoaded();
    }

    private void EnsurePageLoaded()
    {
        DefaultDriverWait
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs(Title));
        Driver.Url.Should().Be(Address);
        Driver.Title.Should().Be(Title);
    }
    
    protected WebDriverWait DefaultDriverWait => new(
        new SystemClock(),
        Driver,
        TimeSpan.FromSeconds(4),
        TimeSpan.FromMilliseconds(100));
}