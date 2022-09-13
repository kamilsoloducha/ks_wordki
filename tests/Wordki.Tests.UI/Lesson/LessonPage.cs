using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Wordki.Tests.UI.Utils;

namespace Wordki.Tests.UI.Lesson;

class LessonPage : Page
{
    public const string LESSON_TITLE = "Wordki - Lesson";
    public const string LESSON_URL = "/lesson";
    
    public LessonPage(IWebDriver driver, string host) : base(driver, LESSON_TITLE, LESSON_URL, host)
    {
    }
    
    public void WaitForLoaded() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.ElementIsVisible(By.ClassName("lesson-page")));

    public IWebElement StartButton => Driver.FindElement(By.XPath("//*[text()='Start']"));


}