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
    
    public void WaitForLoaded() => DefaultDriverWait
        .Until(ExpectedConditions.ElementIsVisible(By.ClassName("lesson-page")));

    public IWebElement StartButton => Driver.FindElement(By.XPath("//*[text()='Start']"));
    public IWebElement CheckButton => Driver.FindElement(By.XPath("//*[text()='Check']"));
    
    public IWebElement AnswerInput => Driver.FindElement(By.Id("answer"));
    public IWebElement WrongButton => 
        Driver.FindElement(By.ClassName("repeats-controller-container"))
        .FindElement(By.ClassName("wrong"));
    
    public IWebElement CorrectButton => 
        Driver.FindElement(By.ClassName("repeats-controller-container"))
            .FindElement(By.ClassName("correct"));

    public IWebElement LessonInfo => Driver.FindElement(By.ClassName("lesson-information"));
    public IWebElement Remaining => LessonInfo.FindElements(By.ClassName("lesson-information-item"))[0].FindElement(By.ClassName("value"));
    public IWebElement Counter => LessonInfo.FindElements(By.ClassName("lesson-information-item"))[1].FindElement(By.ClassName("value"));
    public IWebElement Correct => LessonInfo.FindElements(By.ClassName("lesson-information-item"))[2].FindElement(By.ClassName("value"));
    public IWebElement Accepted => LessonInfo.FindElements(By.ClassName("lesson-information-item"))[3].FindElement(By.ClassName("value"));
    public IWebElement Wrong => LessonInfo.FindElements(By.ClassName("lesson-information-item"))[4].FindElement(By.ClassName("value"));
}