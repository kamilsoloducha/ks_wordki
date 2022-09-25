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
    private readonly DashboardPage _dashboardPage;
    private readonly ErrorPage _errorPage;

    public NavigateToError()
    {
        _dashboardPage = new DashboardPage(Driver, ClientHost);
        _errorPage = new ErrorPage(Driver, ClientHost);
    }

    [SetUp]
    public void Setup()
    {
        Server.AddErrorResponse("/dashboard/summary/userid", 500)
            .AddErrorResponse("/dashboard/forecast", 500);
    }

    void WhenUserNavigateToDashboard() => _dashboardPage.NavigateTo();
    void AndWhenErrorResponseAppears() =>  DefaultDriverWait
        .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs(ErrorPage.ERROR_TITLE));
    void ThenUserSeeErrorPage() => _errorPage.Header.Should().NotBeNull();

    [Test]
    public void ShowErrorPage() => this.BDDfy();
}