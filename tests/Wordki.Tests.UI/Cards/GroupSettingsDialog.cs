using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Wordki.Tests.UI.Cards;

public class GroupSettingsDialog
{
    private readonly IWebDriver _driver;

    public GroupSettingsDialog(IWebDriver driver)
    {
        _driver = driver;
    }

    private IWebElement Dialog => _driver.FindElement(By.ClassName("p-dialog"));

    public void WaitFor() =>
        new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(driver => driver.FindElements(By.ClassName("p-dialog")).Count != 0);

    public IWebElement AddCardButton => _driver.FindElement(By.XPath("//*[text()='Add card']"));
    public IWebElement EditGroupButton => _driver.FindElement(By.XPath("//*[text()='Edit group']"));
}