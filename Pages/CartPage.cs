using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ExactQE.SauceDemo.Pages;

public sealed class CartPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private readonly By CheckoutButton = By.Id("checkout");
    private readonly By BackpackItem = By.Id("item_4_title_link");
    private readonly By BikeLightItem = By.Id("item_0_title_link");

    public CartPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.Url.Contains("cart.html"));

    }

    public bool HasBackpack =>
        driver.FindElements(BackpackItem).Count > 0;

    public bool HasBikeLight =>
        driver.FindElements(BikeLightItem).Count > 0;

    public void Checkout()
{
    wait.Until(d => d.FindElement(CheckoutButton)).Click();

    wait.Until(d => d.Url.Contains("checkout-step-one.html"));
}
}
