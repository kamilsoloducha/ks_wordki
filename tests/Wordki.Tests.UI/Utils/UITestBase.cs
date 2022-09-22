using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using WireMock.Server;
using Wordki.Tests.UI.Login;

namespace Wordki.Tests.UI.Utils;

public abstract class UITestBase : IDisposable
{
    protected string ClientHost { get; } = "http://localhost:3000";
    private string ServiceHost { get; } = "http://*:5000";
    protected ChromeDriver Driver { get; }
    protected WireMockServer Server { get; private set; }

    protected UITestBase()
    {
        var clientHost = Environment.GetEnvironmentVariable("CLIENT_HOST");
        if (!string.IsNullOrEmpty(clientHost)) ClientHost = clientHost;
        
        var serviceHost = Environment.GetEnvironmentVariable("SERVICE_HOST");
        if (!string.IsNullOrEmpty(serviceHost)) ServiceHost = serviceHost;
        
        var headless = Environment.GetEnvironmentVariable("HEADLESS");
        
        var options = new ChromeOptions();
        
        if(!string.IsNullOrEmpty(headless) || true) options.AddArguments("headless");
        
        options.AddArguments("diable-dev-shm-usage",
            "disable-gpu",
            "disable-infobars",
            "ignore-certificate-errors",
            "no-sandbox");
        
        Driver = new ChromeDriver(options);
    }
    
    public void SetAuthorizationCookies()
    {
        new LoginPage(Driver, ClientHost).NavigateTo();
        Driver.ExecuteScript("localStorage.setItem(\"id\", \"userid\");");
        Driver.ExecuteScript("localStorage.setItem(\"token\", \"token\");");
    }

    [SetUp]
    protected void SetupUtils()
    {
        Server = WireMockFactory.Create(ServiceHost);
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