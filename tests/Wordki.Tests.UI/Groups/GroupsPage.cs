using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Wordki.Tests.UI.Groups;

class GroupsPage : Page
{
    protected override string Url => "http://wordki.ui.clinet:81/groups";
    protected override string Title => "React App";
    public GroupsPage(IWebDriver driver) : base(driver) { }

    public IWebElement CreateNewGroupButton => Driver.FindElement(By.XPath("//*[text()='Create new group']"));
    public IEnumerable<IWebElement> Groups => Driver.FindElements(By.ClassName("group-row-container"));
    public void WaitForInitialLoad() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.ElementIsVisible(By.ClassName("groups-action-container")));
}