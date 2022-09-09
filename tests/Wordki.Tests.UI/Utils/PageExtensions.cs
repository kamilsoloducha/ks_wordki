using OpenQA.Selenium;

namespace Wordki.Tests.UI.Utils;

public static class PageExtensions
{
    public static void InsertIntoInput(this IWebElement element, string text, bool append)
    {
        if (!append) element.Clear();
        element.SendKeys(text);
    }
}