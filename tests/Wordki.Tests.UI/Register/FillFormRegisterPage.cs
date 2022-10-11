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
    private static readonly DateTime _today = new (2022, 2, 2);

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
                id = "userid",
            },
            b => true
        );
        Server.AddPutEndpoint(
            "/users/login",
            new
            {
                responseCode = 0,
                id = "userid",
                token = "userid",
                creatingDateTime = new DateTime(),
                expirationDateTime = new DateTime()
            },
            b => true
        );
        Server.AddGetEndpoint(
                "/dashboard/summary/userid",
                new { groupsCount = 10, cardsCount = 20, dailyRepeats = 30 })
            .AddGetEndpoint(
                "/dashboard/forecast",
                new object[]
                {
                    new { Count = 0, Date = _today },
                    new { Count = 0, Date = _today.AddDays(1) },
                    new { Count = 0, Date = _today.AddDays(2) },
                    new { Count = 0, Date = _today.AddDays(3) },
                    new { Count = 0, Date = _today.AddDays(4) },
                    new { Count = 0, Date = _today.AddDays(5) },
                    new { Count = 0, Date = _today.AddDays(6) },
                }
            );
    }

    void GivenLogoutUser() => LogoutUser();
    void AndGivenRegisterPage() => _page.NavigateTo();

    void WhenUserFillUserName() => _page.UserNameInput.InsertIntoInput("testUserName", false);
    void AndWhenUserFillEmail() => _page.EmailInput.InsertIntoInput("test@mail.com", false);
    void AndWhenUserFillPassword() => _page.PasswordInput.InsertIntoInput("testPassword", false);

    void AndWhenUserFillConfirmationPassword() =>
        _page.PasswordConfirmationInput.InsertIntoInput("testPassword", false);

    void AndWhenUserClickSubmitButton()
    {
        _page.Submit.Click();
        DefaultDriverWait
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
    public void CreateAccount() => this.BDDfy();

    [TearDown]
    public void TearDown()
    {
        IsLogin = true;
    }
}