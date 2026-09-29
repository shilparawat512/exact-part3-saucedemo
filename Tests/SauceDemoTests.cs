using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ExactQE.SauceDemo.Pages;

namespace ExactQE.SauceDemo.Tests;

[TestFixture]
public class SauceDemoTests
{
    private const string BaseUrl = "https://www.saucedemo.com/";
    private const string ValidUsername = "standard_user";
    private const string ValidPassword = "secret_sauce";

    private IWebDriver driver = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1920,1080");

        driver = new ChromeDriver(options);
    }

[TearDown]
public void TearDown()
{
    try
    {
        if (driver is ITakesScreenshot screenshotDriver)
        {
            var directory = Path.Combine(
                Directory.GetCurrentDirectory(),
                "TestResults",
                "Screenshots");

            Directory.CreateDirectory(directory);

            var path = Path.Combine(
                directory,
                $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            screenshotDriver.GetScreenshot().SaveAsFile(path);
            TestContext.AddTestAttachment(path);
        }
    }
    finally
    {
        driver?.Quit();
        driver?.Dispose();
    }
}

[OneTimeSetUp]
public void CleanScreenshots()
{
    var screenshotsDirectory = Path.Combine(
    Directory.GetCurrentDirectory(),
    "TestResults",
    "Screenshots");

    if (Directory.Exists(screenshotsDirectory))
    {
        Directory.Delete(screenshotsDirectory, true);
    }

    Directory.CreateDirectory(screenshotsDirectory);
}

    [Test]
    public void ValidLogin_ShouldOpenProductsPage()
    {
        var login = new LoginPage(driver);
        login.Open(BaseUrl);
        login.Login(ValidUsername, ValidPassword);

        var inventory = new InventoryPage(driver);

        Assert.That(inventory.Title, Is.EqualTo("Products"),
            "A successful login should open the Products page.");
    }

    [Test]
    public void InvalidLogin_ShouldDisplayError()
    {
        var login = new LoginPage(driver);
        login.Open(BaseUrl);
        login.Login("invalid_user", "invalid_password");

        Assert.That(login.ErrorText, Does.Contain("Username and password do not match any user in this service"),
            "An invalid login should display the authentication error.");
    }

    [Test]
    public void AddProducts_ShouldUpdateCart()
    {
        var login = new LoginPage(driver);
        login.Open(BaseUrl);
        login.Login(ValidUsername, ValidPassword);

        var inventory = new InventoryPage(driver);
        inventory.AddBackpack();
        inventory.AddBikeLight();

        Assert.That(inventory.CartCount, Is.EqualTo("2"),
            "The cart badge should show the number of selected products.");

        inventory.OpenCart();

        var cart = new CartPage(driver);

        Assert.Multiple(() =>
        {
            Assert.That(cart.HasBackpack, Is.True, "Backpack should be present in the cart.");
            Assert.That(cart.HasBikeLight, Is.True, "Bike Light should be present in the cart.");
        });
    }

    [Test]
    public void Checkout_ShouldCompleteOrderSuccessfully()
    {
        var login = new LoginPage(driver);
        login.Open(BaseUrl);
        login.Login(ValidUsername, ValidPassword);

        var inventory = new InventoryPage(driver);
        inventory.AddBackpack();
        inventory.OpenCart();

        var cart = new CartPage(driver);
        cart.Checkout();

        var checkout = new CheckoutPage(driver);
        checkout.EnterCustomerDetails("Test", "Customer", "12345");
        checkout.FinishOrder();

        Assert.That(checkout.CompletionMessage,
            Is.EqualTo("Thank you for your order!"),
            "Successful checkout should display the order confirmation.");
    }
}
