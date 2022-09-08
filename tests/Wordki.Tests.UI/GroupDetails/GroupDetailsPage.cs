using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Wordki.Tests.UI.GroupDetails;

class GroupDetailsPage : Page
{
    protected override string Url => "http://wordki.ui.clinet:81/cards/1";
    protected override string Title => "React App";
    public GroupDetailsPage(IWebDriver driver) : base(driver) { }

    public IEnumerable<IWebElement> Cards => Driver.FindElements(By.ClassName("card-item"));
    IWebElement Paginator => Driver.FindElement(By.ClassName("group-details-paginator"));
    IWebElement Search => Paginator.FindElement(By.CssSelector("imput"));
    IWebElement Extendable => Driver.FindElement(By.ClassName("expandable-container-button"));
    public IWebElement AllCards => Driver.FindElements(By.ClassName("group-details-info-card"))[0];
    public IWebElement Learning => Driver.FindElements(By.ClassName("group-details-info-card"))[1];
    public IWebElement Drawer1 => Driver.FindElements(By.ClassName("group-details-info-card"))[2];
    public IWebElement Drawer2 => Driver.FindElements(By.ClassName("group-details-info-card"))[3];
    public IWebElement Drawer3 => Driver.FindElements(By.ClassName("group-details-info-card"))[4];
    public IWebElement Drawer4 => Driver.FindElements(By.ClassName("group-details-info-card"))[5];
    public IWebElement Drawer5 => Driver.FindElements(By.ClassName("group-details-info-card"))[6];
    public IWebElement Waiting => Driver.FindElements(By.ClassName("group-details-info-card"))[7];
    public IWebElement Ticks => Driver.FindElements(By.ClassName("group-details-info-card"))[8];

    public void SearchCard(string text)
    {
        var search = Search;
        search.Clear();
        search.SendKeys(text);
    }

    public void ExtendsInfo() => Extendable.Click();
    public void WaitForInitialLoad() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.ElementIsVisible(By.ClassName("group-details-container")));

    public void OpenAddCardDialog()
    {
        Driver.FindElement(By.ClassName("group-details-settings")).Click();
        new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
            .Until(ExpectedConditions.ElementIsVisible(By.ClassName("p-dialog")));

        var actionDialog = Driver.FindElement(By.ClassName("p-dialog"));
        actionDialog.FindElement(By.XPath("//*[text()='Add card']")).Click();
        Thread.Sleep(200);
    }

    public void OpenEditGroupDialog()
    {
        Driver.FindElement(By.ClassName("group-details-settings")).Click();
        new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
            .Until(ExpectedConditions.ElementIsVisible(By.ClassName("p-dialog")));

        var actionDialog = Driver.FindElement(By.ClassName("p-dialog"));
        actionDialog.FindElement(By.XPath("//*[text()='Edit group']")).Click();
        Thread.Sleep(200);
    }

}