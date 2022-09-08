using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Wordki.Tests.UI.CardDialog;

class CardDialogPage : Page
{
    public CardDialogPage(IWebDriver driver) : base(driver) { }

    IWebElement CardDialog => Driver.FindElement(By.ClassName("p-dialog"));
    public IWebElement FrontValue => CardDialog.FindElement(By.CssSelector("input[name=frontValue]"));
    public IWebElement BackValue => CardDialog.FindElement(By.CssSelector("input[name=backValue]"));
    public IWebElement FrontExample => CardDialog.FindElement(By.CssSelector("input[name=frontExample]"));
    public IWebElement BackExample => CardDialog.FindElement(By.CssSelector("input[name=backExample]"));
    public IWebElement FrontEnabled => CardDialog.FindElement(By.CssSelector("input[name=frontEnabled]"));
    public IWebElement BackEnabled => CardDialog.FindElement(By.CssSelector("input[name=backEnabled]"));
    public IWebElement Comment => CardDialog.FindElement(By.CssSelector("input[name=comment]"));
    public IWebElement IsTicked => CardDialog.FindElement(By.CssSelector("input[name=isTicked]"));

    public IWebElement DeleteButton => CardDialog.FindElement(By.XPath("//*[text()='Delete']"));
    public IWebElement SaveButton => CardDialog.FindElement(By.XPath("//*[text()='Save']"));
    public void WaitForInitialLoad() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.ElementIsVisible(By.ClassName("p-dialog")));

    public void WaitForFinish() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("p-dialog")));
}