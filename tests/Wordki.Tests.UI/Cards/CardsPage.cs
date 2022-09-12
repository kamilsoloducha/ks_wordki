using System.Collections.Generic;
using OpenQA.Selenium;

namespace Wordki.Tests.UI.Cards;

public sealed class CardsPage : Utils.Page
{
    public const string GROUP_ID = "groupId";
    public const string GROUP_NAME = "GroupName";
    public const string CARDS_TITLE = "Wordki - " + GROUP_NAME;
    public const string CARDS_PATH = "/cards/" + GROUP_ID;

    public CardsPage(IWebDriver driver, string host) : base(driver, CARDS_TITLE, CARDS_PATH, host)
    {
    }

    public IEnumerable<IWebElement> Cards => Driver.FindElements(By.ClassName("card-item"));
}