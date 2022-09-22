using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Wordki.Tests.UI.Utils;

public abstract class Page
{
    protected IWebDriver Driver { get; }
    private string Url { get; }
    private string Host { get; }
    private string Address => Host + Url;
    private string Title { get; }

    protected Page(IWebDriver driver, string title, string url, string host)
    {
        Driver = driver;
        Title = title;
        Url = url;
        Host = host;
    }

    public void NavigateTo()
    {
        var stopWatch = Stopwatch.StartNew();
        Driver.Navigate().GoToUrl(Address);
        EnsurePageLoaded();
        stopWatch.Stop();
        Console.WriteLine($"Navigation to {Address} took {stopWatch.ElapsedMilliseconds} ms");
    }

    private void EnsurePageLoaded()
    {
        new WebDriverWait(new SystemClock(), Driver, TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(100))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs(Title));
    }
}