using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using TestStack.BDDfy;
using Wordki.Tests.UI.Dashboard;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Error;

[TestFixture]
public class NavigateToError : UITestBase
{
    private DashboardPage _dashboardPage;
    private ErrorPage _errorPage;

    [SetUp]
    public void Setup()
    {
        _dashboardPage = new DashboardPage(Driver, ClientHost);
        _errorPage = new ErrorPage(Driver, ClientHost);
        Server.AddErrorResponse("/dashboard/summary/userid", 500)
            .AddErrorResponse("/dashboard/forecast", 500);
    }

    void GivenLoginUser() => LoginUser();
    
    void WhenUserNavigateToDashboard() => _dashboardPage.NavigateTo();

    void AndWhenErrorResponseAppears() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs(ErrorPage.ERROR_TITLE));

    void ThenUserSeeErrorPage() => _errorPage.Header.Should().NotBeNull();

    [Test]
    public void Test() => this.BDDfy();
}