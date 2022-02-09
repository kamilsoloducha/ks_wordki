using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using WireMock.Server;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenQA.Selenium.Support.UI;

namespace Wordki.Tests.UI
{
    public abstract class UITestBase : IDisposable
    {
        protected string AppUrl = "http://localhost:3000";
        protected ChromeDriver Driver { get; private set; }
        protected WireMockServer Server { get; private set; }

        protected UITestBase()
        {
            // AppUrl = Environment.GetEnvironmentVariable("ClientUrl");

            var options = new ChromeOptions();
            // options.AddArguments("headless");
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
            Server = WireMockFactory.Create("http://*:5000");
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
            Driver.Navigate().GoToUrl($"http://localhost:3000/login");
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


}
