using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ExactQE.SauceDemo.Pages;

public sealed class InventoryPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private readonly By PageTitle = By.CssSelector("[data-test='title']");
    private readonly By CartLink = By.CssSelector("[data-test='shopping-cart-link']");
    private readonly By BackpackAddButton = By.Id("add-to-cart-sauce-labs-backpack");
    private readonly By BikeLightAddButton = By.Id("add-to-cart-sauce-labs-bike-light");
    private readonly By CartBadge = By.CssSelector("[data-test='shopping-cart-badge']");

    public InventoryPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public string Title => wait.Until(d => d.FindElement(PageTitle)).Text;

    public void AddBackpack() =>
        wait.Until(d => d.FindElement(BackpackAddButton)).Click();

    public void AddBikeLight() =>
        wait.Until(d => d.FindElement(BikeLightAddButton)).Click();

    public string CartCount =>
        wait.Until(d => d.FindElement(CartBadge)).Text;

    public void OpenCart()
{
    wait.Until(d => d.FindElement(CartLink)).Click();
    wait.Until(d => d.Url.Contains("cart.html"));
}
}
