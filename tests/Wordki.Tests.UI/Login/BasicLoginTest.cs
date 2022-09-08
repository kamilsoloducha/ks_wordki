using NUnit.Framework;
using System.Threading;
using TestStack.BDDfy;

namespace Wordki.Tests.UI.Login;

[TestFixture(Ignore = "not ready")]
public class BasicLoginTest : UITestBase
{
    private readonly LoginPage _page;

    public BasicLoginTest()
    {
        _page = new LoginPage(Driver);
    }

    void WhenUserNavigateToLoginPage()
    {
        Driver.Navigate().GoToUrl($"{AppUrl}/login");
        Thread.Sleep(200);
    }

    void ThenTitleShouldBeSet()
    {
        Assert.AreEqual("Wordki - Login", Driver.Title);
    }

    [Test]
    public void ExecuteTest()
    {
        this.BDDfy();
    }
}