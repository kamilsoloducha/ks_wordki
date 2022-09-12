using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Dashboard;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Register;

[TestFixture]
public class FillFormRegisterPage : Utils.UITestBase
{
    private readonly RegisterPage _page;

    public FillFormRegisterPage()
    {
        _page = new RegisterPage(Driver, ClientHost);
    }

    [SetUp]
    public void Setup()
    {
        Server.AddPostEndpoint(
            "/users/register",
            new
            {
                responseCode = 0,
                id = "string",
            },
            b => true
        );
        Server.AddPutEndpoint(
            "/users/login",
            new
            {
                responseCode = 0,
                id = "string",
                token = "string",
                creatingDateTime = new DateTime(),
                expirationDateTime = new DateTime()
            },
            b => true
        );
    }

    void GivenRegisterPage() => Driver.Navigate().GoToUrl(_page.Address);

    void WhenUserFillUserName() => _page.UserNameInput.InsertIntoInput("testUserName", false);
    void AndWhenUserFillEmail() => _page.EmailInput.InsertIntoInput("test@mail.com", false);
    void AndWhenUserFillPassword() => _page.PasswordInput.InsertIntoInput("testPassword", false);

    void AndWhenUserFillConfirmationPassword() =>
        _page.PasswordConfirmationInput.InsertIntoInput("testPassword", false);

    void AndWhenUserClickSubmitButton()
    {
        _page.Submit.Click();
        new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs(DashboardPage.DASHBOARD_TITLE));
    }

    void ThenUserShouldBeRedirectToDashboard() => Driver.Title.Should().Be(DashboardPage.DASHBOARD_TITLE);

    void AndThenServerShouldReceivedRegisterRequest() =>
        Server.LogEntries.Should().Contain(x => x.RequestMessage.Method == HttpMethod.Post.Method &&
                                                x.RequestMessage.Path == "/users/register");
    
    void AndThenServerShouldReceivedLoginRequest() =>
        Server.LogEntries.Should().Contain(x => x.RequestMessage.Method == HttpMethod.Put.Method &&
                                                x.RequestMessage.Path == "/users/login");
    
    [Test]
    public void Test() => this.BDDfy();
}