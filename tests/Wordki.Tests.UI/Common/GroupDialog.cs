using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Common;

public sealed class GroupDialog
{
    private readonly IWebDriver _driver;

    public GroupDialog(IWebDriver driver)
    {
        _driver = driver;
    }

    IWebElement Dialog => _driver.FindElement(By.ClassName("p-dialog"));
    public IWebElement GroupName => Dialog.FindElement(By.CssSelector("input[name=name]"));

    private IWebElement FrontLanguage => Dialog.FindElements(By.ClassName("dialog-form-item"))[1];
    public void SelectFront(int index)
    {
        FrontLanguage.Click();
        var dropdownPanel = _driver.FindElement(By.ClassName("p-dropdown-panel"));
        var item = dropdownPanel.FindElements(By.CssSelector("li"))[index];
        item.Click();
    }

    private IWebElement BackLanguage => Dialog.FindElements(By.ClassName("dialog-form-item"))[2];
    public void SelectBack(int index)
    {
        BackLanguage.Click();
        var dropdownPanel = _driver.FindElement(By.ClassName("p-dropdown-panel"));
        var item = dropdownPanel.FindElements(By.CssSelector("li"))[index];
        item.Click();
    }

    public IWebElement SaveButton => Dialog.FindElement(By.XPath("//*[text()='Save']"));

    public void WaitForFinish() => new WebDriverWait(_driver, TimeSpan.FromSeconds(2))
        .Until(driver => driver.FindElements(By.ClassName("p-dialog")).Count == 0);

    public void FillFormWith(string groupName, int frontLanguage, int backLanguage)
    {
        GroupName.InsertIntoInput(groupName, false);
        SelectFront(frontLanguage);
        SelectBack(backLanguage);
    }

    public void SaveAndWait()
    {
        SaveButton.Click();
        WaitForFinish();
    }
}