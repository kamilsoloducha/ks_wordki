using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Wordki.Tests.UI.Lesson
{
    class LessonPage : Page
    {
        protected override string Url => "http://localhost:3000/lesson";
        public LessonPage(IWebDriver driver) : base(driver)
        {
        }

        public void WaitForLoaded() => new WebDriverWait(Driver, TimeSpan.FromSeconds(2))
                .Until(ExpectedConditions.ElementIsVisible(By.ClassName("lesson-page")));

    }
}