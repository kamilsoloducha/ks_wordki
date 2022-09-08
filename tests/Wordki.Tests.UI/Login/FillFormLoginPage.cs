using NUnit.Framework;
using TestStack.BDDfy;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;
using System;

namespace Wordki.Tests.UI.Login;

[TestFixture]
public class FillFormLoginPage : UITestBase
{
    private readonly LoginPage _page;

    public FillFormLoginPage()
    {
        _page = new LoginPage(Driver);
    }
    void GivenSetupServer()
    {
        Server.AddPutEndpoint(
            "/users/login",
            new
            {
                responseCode = 0,
                id= "string",
                token= "string",
                creatingDateTime =  new DateTime(),
                expirationDateTime=  new DateTime()
            },
            b => true
        );
    }
    void WhenUserNavigateToLoginPage() => _page.NavigateTo();
    void AndWhenUserFillUserNameField() => _page.InsertIntoInput(_page.UserNameInput, "testUserName", false);
    void AndWhenUserFillPasswordField() => _page.InsertIntoInput(_page.PasswordInput, "testPassword", false);
    void AndWhenUserClickSubmit()
    {
        _page.Submit.Click();
        new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs("Wordki - Dashboard"));
    }
    void ThenUserShouldBeRedirectToDashboard() => Assert.AreEqual("Wordki - Dashboard", Driver.Title);
    void AndThenServerShouldRecivedRequests() => Server.LogEntries.Should().HaveCount(4);

    [Test]
    public void ExecuteTest() => this.BDDfy();

}