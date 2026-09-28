using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ExactQE.SauceDemo.Pages;

public sealed class CheckoutPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private readonly By FirstName = By.Id("first-name");
    private readonly By LastName = By.Id("last-name");
    private readonly By PostalCode = By.Id("postal-code");
    private readonly By ContinueButton = By.Id("continue");
    private readonly By FinishButton = By.Id("finish");
    private readonly By CompleteHeader = By.CssSelector("[data-test='complete-header']");
    private readonly By ErrorMessage = By.CssSelector("[data-test='error']");

    public CheckoutPage(IWebDriver driver)
{
    this.driver = driver;
    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

    wait.Until(d => d.Url.Contains("checkout-step-one.html"));
}

  public void EnterCustomerDetails(string firstName, string lastName, string postalCode)
{
   wait.Until(d => d.FindElement(FirstName)).SendKeys(firstName);
    wait.Until(d => d.FindElement(LastName)).SendKeys(lastName);
    wait.Until(d => d.FindElement(PostalCode)).SendKeys(postalCode);

    wait.Until(d => d.FindElement(ContinueButton)).Click();

    wait.Until(d => d.Url.Contains("checkout-step-two.html"));
}

public void FinishOrder()
{
    wait.Until(d => d.FindElement(FinishButton)).Click();

    // Wait until order completion page is loaded
    wait.Until(d => d.Url.Contains("checkout-complete.html"));
}

    public string CompletionMessage =>
    wait.Until(d => d.FindElement(CompleteHeader)).Text;
    public string ErrorText =>
        driver.FindElement(ErrorMessage).Text;
}
