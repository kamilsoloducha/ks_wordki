using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using WireMock.Server;

namespace Wordki.Tests.UI.Utils;

public abstract class UITestBase : IDisposable
{
    protected string ClientHost { get; } = "http://localhost:81";
    protected ChromeDriver Driver { get; }
    protected WireMockServer Server { get; private set; }

    protected UITestBase()
    {
        var clientHost = Environment.GetEnvironmentVariable("CLIENT_HOST");
        if (!string.IsNullOrEmpty(clientHost)) ClientHost = clientHost;
        
        var headless = Environment.GetEnvironmentVariable("HEADLESS");
        
        var options = new ChromeOptions();
        
        if(!string.IsNullOrEmpty(headless)) options.AddArguments("headless");
        
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
        Server = UI.WireMockFactory.Create("http://*:5001");
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
}