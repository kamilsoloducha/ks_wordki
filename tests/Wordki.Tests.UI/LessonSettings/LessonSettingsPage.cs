// using OpenQA.Selenium;
// using OpenQA.Selenium.Support.UI;
// using System;
// using System.Linq;
//
// namespace Wordki.Tests.UI.LessonSettings;
//
// class LessonSettingsPage : Page
// {
//     protected override string Url => "http://wordki.ui.clinet:81/lesson-settings";
//     public LessonSettingsPage(IWebDriver driver) : base(driver) { }
//
//     private IWebElement TabView => Driver.FindElement(By.ClassName("tab-view-header-container"));
//     public IWebElement RepetitionTab => TabView.FindElements(By.ClassName("tab-view-header-item"))[0];
//     public IWebElement NewWrodsTab => TabView.FindElements(By.ClassName("tab-view-header-item"))[1];
//
//     private IWebElement LanguageSelector => Driver.FindElement(By.ClassName("language-items-container"));
//     public void SelectPolishLanguage() => LanguageSelector.FindElement(By.CssSelector("label[for=polish]")).Click();
//     public void SelectEnglishLanguage() => LanguageSelector.FindElement(By.CssSelector("label[for=english]")).Click();
//
//     public void InsertCardsCount(int count)
//     {
//         var countSelector = Driver.FindElement(By.ClassName("count-container"));
//         var input = countSelector.FindElement(By.CssSelector("input"));
//         input.Clear();
//         input.SendKeys(count.ToString());
//     }
//
//     public void SelectAllCards()
//     {
//         var countSelector = Driver.FindElement(By.ClassName("count-container"));
//         var count = countSelector.FindElement(By.CssSelector("strong"));
//         new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
//             .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(count, "100"));
//
//         var buttons = countSelector.FindElements(By.CssSelector("button"));
//         var allButton = buttons.Last();
//         allButton.Click();
//     }
//
//     private IWebElement TypeSelector => Driver.FindElement(By.ClassName("lesson-type-container"));
//     public void SelectFiszki() => TypeSelector.FindElements(By.CssSelector("label"))[0].Click();
//     public void SelectInserting() => TypeSelector.FindElements(By.CssSelector("label"))[1].Click();
//     public void StartLesson()
//     {
//         var startButton = Driver.FindElement(By.ClassName("settings-container")).FindElement(By.CssSelector("button"));
//         startButton.Click();
//     }
//
//     private IWebElement GroupSelector => Driver.FindElement(By.ClassName("group-selector-container p-dropdown"));
//     public void SelectGroup(int index)
//     {
//         GroupSelector.Click();
//         var dropdownPanel = Driver.FindElement(By.ClassName("p-dropdown-panel"));
//         var item = dropdownPanel.FindElements(By.CssSelector("li"))[index];
//         item.Click();
//     }
// }