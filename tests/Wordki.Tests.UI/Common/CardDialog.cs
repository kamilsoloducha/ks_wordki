using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Common;

public sealed class CardDialog
{
    private readonly IWebDriver _driver;
    public CardDialog(IWebDriver driver)
    {
        _driver = driver;
    }

    IWebElement Dialog => _driver.FindElement(By.ClassName("p-dialog"));
    public IWebElement FrontValue => Dialog.FindElement(By.CssSelector("input[name=frontValue]"));
    public IWebElement BackValue => Dialog.FindElement(By.CssSelector("input[name=backValue]"));
    public IWebElement FrontExample => Dialog.FindElement(By.CssSelector("input[name=frontExample]"));
    public IWebElement BackExample => Dialog.FindElement(By.CssSelector("input[name=backExample]"));
    public IWebElement FrontEnabled => Dialog.FindElement(By.CssSelector("input[name=frontEnabled]"));
    public IWebElement BackEnabled => Dialog.FindElement(By.CssSelector("input[name=backEnabled]"));
    public IWebElement Comment => Dialog.FindElement(By.CssSelector("input[name=comment]"));
    public IWebElement IsTicked => Dialog.FindElement(By.CssSelector("input[name=isTicked]"));

    public IWebElement DeleteButton => Dialog.FindElement(By.XPath("//*[text()='Delete']"));
    public IWebElement SaveButton => Dialog.FindElement(By.XPath("//*[text()='Save']"));
    public void WaitForInitialLoad() => new WebDriverWait(_driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.ElementIsVisible(By.ClassName("p-dialog")));

    public void WaitForFinish() => new WebDriverWait(_driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("p-dialog")));

    public void FillWith(string frontValue, string backValue, bool isUsed, bool isTicked)
    {
        FrontValue.InsertIntoInput(frontValue, false);
        BackValue.InsertIntoInput(backValue, false);
        if (isUsed)
        {
            FrontEnabled.Click();
            BackEnabled.Click();   
        }

        if (isTicked)
        {
            IsTicked.Click();
        }
    }

    public void SaveAndWait()
    {
        SaveButton.Click();
        WaitForFinish();
    }

    public void DeleteAndWait()
    {
        DeleteButton.Click();
        WaitForFinish();
    }
}