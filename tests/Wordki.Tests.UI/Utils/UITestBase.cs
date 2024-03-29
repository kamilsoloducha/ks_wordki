using System;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WireMock.Server;
using Wordki.Tests.UI.Login;

namespace Wordki.Tests.UI.Utils;

public abstract class UITestBase
{
    private static ChromeDriver _driver;
    private static WireMockServer _server;
    protected static bool IsLogin;
    protected string ClientHost { get; } = "http://localhost:3000";
    private string ServiceHost { get; } = "http://*:5000";
    protected ChromeDriver Driver => _driver;
    protected WireMockServer Server => _server;

    protected UITestBase()
    {
        var clientHost = Environment.GetEnvironmentVariable("CLIENT_HOST");
        if (!string.IsNullOrEmpty(clientHost)) ClientHost = clientHost;

        var serviceHost = Environment.GetEnvironmentVariable("SERVICE_HOST");
        if (!string.IsNullOrEmpty(serviceHost)) ServiceHost = serviceHost;

        if (_driver == null)
        {
            var headless = Environment.GetEnvironmentVariable("HEADLESS");
            var options = new ChromeOptions();
            if (!string.IsNullOrEmpty(headless)) options.AddArguments("headless");
            options.AddArguments("disable-dev-shm-usage",
                "disable-gpu",
                "disable-popup-blocking",
                "disable-infobars",
                "ignore-certificate-errors",
                "no-sandbox");

            _driver = new ChromeDriver(options);
        }

        _server ??= WireMockFactory.Create(ServiceHost);
    }

    protected void LoginUser()
    {
        if (IsLogin) return;
        IsLogin = true;

        new LoginPage(Driver, ClientHost).NavigateTo();
        Driver.ExecuteScript("localStorage.setItem(\"id\", \"userid\");");
        Driver.ExecuteScript("localStorage.setItem(\"token\", \"token\");");
    }

    protected void LogoutUser()
    {
        if (!IsLogin) return;
        IsLogin = false;
        Driver.Navigate().GoToUrl(ClientHost + "/logout");
        Driver.ExecuteScript("localStorage.clear();");
    }

    [TearDown]
    protected void TearDownUtils()
    {
        _server.Reset();
    }

    protected WebDriverWait DefaultDriverWait => new(
        new SystemClock(),
        Driver,
        TimeSpan.FromSeconds(4),
        TimeSpan.FromMilliseconds(100));
}