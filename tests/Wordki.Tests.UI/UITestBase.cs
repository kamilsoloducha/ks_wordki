using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using WireMock.Server;
using OpenQA.Selenium.Support.UI;

namespace Wordki.Tests.UI;

public abstract class UITestBase : IDisposable
{
    public static string AppUrl = "http://localhost:81";
    protected ChromeDriver Driver { get; }
    protected WireMockServer Server { get; private set; }

    protected UITestBase()
    {
        var test = Environment.GetEnvironmentVariable("TEST");
        var clientUrl =  Environment.GetEnvironmentVariable("ClientUrl");
        if (!string.IsNullOrEmpty(clientUrl)) AppUrl = clientUrl;
        
        Console.WriteLine($"AppUrl - '{AppUrl}'");
        
        var options = new ChromeOptions();
        options.AddArguments("headless");
        options.AddArguments("diable-dev-shm-usage",
            "disable-gpu",
            "disable-infobars",
            "ignore-certificate-errors",
            "no-sandbox");
        Driver = new ChromeDriver(options);
    }

    [SetUp]
    protected void SetupUtils()
    {
        Server = WireMockFactory.Create("http://*:5001");
    }

    [TearDown]
    protected void TearDownUtils()
    {
        Server.Stop();
        Server.Dispose();

        Dispose();
    }

    public void Dispose()
    {
        Driver.Quit();
        Driver.Dispose();
    }

    protected void SetAuthorizationCookie()
    {
        Driver.Navigate().GoToUrl($"http://wordki.ui.client:81/login");
        new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs("Wordki - Login"));
        Driver.ExecuteScript("localStorage.setItem(\"id\", \"userid\");");
        Driver.ExecuteScript("localStorage.setItem(\"token\", \"token\");");
    }

    protected void SetupDefaultGroupEndpoints() =>
        Server.AddGetEndpoint("/group/all", new object[0]);

    protected void SetupDefaultCardEndpoints() =>
        Server.AddGetEndpoint("/group/details/1", new { })
            .AddGetEndpoint("/card/all/1", new object[0]);
}