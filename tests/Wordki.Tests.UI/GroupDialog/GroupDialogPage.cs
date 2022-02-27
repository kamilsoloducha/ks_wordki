using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Wordki.Tests.UI.GroupDialog
{
    class GroupDialogPage : Page
    {
        public GroupDialogPage(IWebDriver driver) : base(driver) { }

        IWebElement Dialog => Driver.FindElement(By.ClassName("p-dialog"));
        public IWebElement GroupName => Dialog.FindElement(By.CssSelector("input[name=name]"));

        private IWebElement FrontLangauge => Dialog.FindElements(By.ClassName("dialog-form-item"))[1];
        public void SelectFront(int index)
        {
            FrontLangauge.Click();
            var dropdownPanel = Driver.FindElement(By.ClassName("p-dropdown-panel"));
            var item = dropdownPanel.FindElements(By.CssSelector("li"))[index];
            item.Click();
        }

        private IWebElement BackLangauge => Dialog.FindElements(By.ClassName("dialog-form-item"))[2];
        public void SelectBack(int index)
        {
            BackLangauge.Click();
            var dropdownPanel = Driver.FindElement(By.ClassName("p-dropdown-panel"));
            var item = dropdownPanel.FindElements(By.CssSelector("li"))[index];
            item.Click();
        }

        public IWebElement SaveButton => Dialog.FindElement(By.XPath("//*[text()='Save']"));

        public void WaitForFinish() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
                .Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("p-dialog")));
    }
}