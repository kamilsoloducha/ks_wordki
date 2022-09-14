using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Wordki.Tests.UI.Groups;

class GroupsPage : Utils.Page
{
    public const string GROUPS_TITLE = "Wordki - Groups";
    public const string GROUPS_PATH = "/groups";
    public GroupsPage(IWebDriver driver, string host) : base(driver, GROUPS_TITLE, GROUPS_PATH, host) { }

    public IWebElement CreateNewGroupButton => Driver.FindElement(By.XPath("//*[text()='Create new group']"));
    public IEnumerable<IWebElement> Groups => Driver.FindElements(By.ClassName("group-row-container"));
    public void WaitForInitialLoad() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.ElementIsVisible(By.ClassName("groups-action-container")));
}