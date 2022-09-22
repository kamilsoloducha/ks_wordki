using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using WireMock.Server;
using Wordki.Tests.UI.Login;

namespace Wordki.Tests.UI.Utils;

public abstract class UITestBase
{
    private static ChromeDriver _driver;
    private static WireMockServer _server;
    private static bool _isLogin;
    
    protected string ClientHost { get; private set; } = "http://localhost:3000";
    protected string ServiceHost { get; private set; } = "http://*:5000";
    protected ChromeDriver Driver => _driver;
    protected WireMockServer Server => _server;

    protected void LoginUser()
    {
        if (_isLogin) return;
        _isLogin = true;
        new LoginPage(Driver, ClientHost).NavigateTo();
        Driver.ExecuteScript("localStorage.setItem(\"id\", \"userid\");");
        Driver.ExecuteScript("localStorage.setItem(\"token\", \"token\");");
    }

    protected void LogoutUser()
    {
        _isLogin = false;
        Driver.ExecuteScript("localStorage.clear();");
    } 

    [SetUp]
    protected void SetupUtils()
    {
        DriverSetup();
        ServerSetup();
    }

    [TearDown]
    protected void TearDownUtils()
    {
        Server.Reset();
    }

    private void DriverSetup()
    {
        if (_driver is not null) return;
        
        var clientHost = Environment.GetEnvironmentVariable("CLIENT_HOST");
        if (!string.IsNullOrEmpty(clientHost)) ClientHost = clientHost;
        
        var serviceHost = Environment.GetEnvironmentVariable("SERVICE_HOST");
        if (!string.IsNullOrEmpty(serviceHost)) ServiceHost = serviceHost;
        
        var headless = Environment.GetEnvironmentVariable("HEADLESS");
        
        var options = new ChromeOptions();
        
        if(!string.IsNullOrEmpty(headless)||true) options.AddArguments("headless");
        
        options.AddArguments("diable-dev-shm-usage",
            "disable-gpu",
            "disable-infobars",
            "ignore-certificate-errors",
            "no-sandbox");
        
        _driver = new ChromeDriver(options);
    }

    private void ServerSetup()
    {
        if (_server is not null) return;
        
        _server = WireMockFactory.Create(ServiceHost);
    }
}