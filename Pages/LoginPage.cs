using OpenQA.Selenium;

namespace ExactQE.SauceDemo.Pages;

public sealed class LoginPage
{
    private readonly IWebDriver driver;

    private readonly By Username = By.Id("user-name");
    private readonly By Password = By.Id("password");
    private readonly By LoginButton = By.Id("login-button");
    private readonly By ErrorMessage = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver) => this.driver = driver;

    public void Open(string baseUrl) => driver.Navigate().GoToUrl(baseUrl);

    public void Login(string username, string password)
    {
        driver.FindElement(Username).SendKeys(username);
        driver.FindElement(Password).SendKeys(password);
        driver.FindElement(LoginButton).Click();
    }

    public string ErrorText => driver.FindElement(ErrorMessage).Text;
}
