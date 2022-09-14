using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Wordki.Tests.UI.Dashboard;

class DashboardPage : Utils.Page
{
    public const string DASHBOARD_TITLE = "Wordki - Dashboard";
    public const string DASHBAORD_URL = "/dashboard";
    public DashboardPage(IWebDriver driver, string host) : base(driver, DASHBOARD_TITLE, DASHBAORD_URL, host)
    {
    }
    
    public IEnumerable<IWebElement> Infos => Driver.FindElements(By.ClassName("info-container"));
    public IWebElement Repeats => Infos.First(x => x.Text.Contains("Repeats"));
    public IWebElement Groups => Infos.First(x => x.Text.Contains("Groups"));
    public IWebElement Cards => Infos.First(x => x.Text.Contains("Cards"));

    
}