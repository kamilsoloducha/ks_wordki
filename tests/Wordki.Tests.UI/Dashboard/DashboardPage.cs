using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Wordki.Tests.UI.Login;

class DashboardPage : Page
{
    protected override string Url => "http://wordki.ui.clinet:81/dashboard";
    protected override string Title => "Wordki - Dashboard";

    public DashboardPage(IWebDriver driver) : base(driver) { }
    public IEnumerable<IWebElement> Infos => Driver.FindElements(By.ClassName("info-container"));
    public IWebElement Repeats => Infos.First(x => x.Text.Contains("Repeats"));
    public IWebElement Groups => Infos.First(x => x.Text.Contains("Groups"));
    public IWebElement Cards => Infos.First(x => x.Text.Contains("Cards"));
}