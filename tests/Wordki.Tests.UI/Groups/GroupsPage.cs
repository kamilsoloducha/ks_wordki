using OpenQA.Selenium;

namespace Wordki.Tests.UI.Groups
{
    class GroupsPage : Page
    {
        protected override string Url => "http://localhost:3000/groups";
        public GroupsPage(IWebDriver driver) : base(driver) { }
    }
}