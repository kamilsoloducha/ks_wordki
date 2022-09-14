using OpenQA.Selenium;

namespace Wordki.Tests.UI.Register;

public class RegisterPage : Utils.Page
{
    public const string REGISTER_URL = "/register";
    public const string REGISTER_TITLE = "Wordki - Register";

    public RegisterPage(IWebDriver driver, string host) : base(driver, REGISTER_TITLE, REGISTER_URL, host)
    {
    }
    
    public IWebElement UserNameInput => Driver.FindElement(By.Id("userName"));
    public IWebElement EmailInput => Driver.FindElement(By.Id("email"));
    public IWebElement PasswordInput => Driver.FindElement(By.Id("password"));
    public IWebElement PasswordConfirmationInput => Driver.FindElement(By.Id("passwordConfirmation"));
    public IWebElement Submit => Driver.FindElement(By.CssSelector("input[type=submit]"));
}