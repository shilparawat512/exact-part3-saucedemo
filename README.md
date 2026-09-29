# Exact Online QE Assessment – Part 3

## Scope

This solution implements Part 3 of the Exact Online Quality Engineer assessment using:

- C#
- .NET 8
- Selenium WebDriver
- NUnit
- Page Object Model

The test application is SauceDemo.

## Covered flows

1. Successful login
2. Invalid login
3. Add multiple products to the cart and verify cart contents
4. Complete checkout and verify the order confirmation

## Project structure

```text
ExactQE.SauceDemo/
├── Pages/
│   ├── LoginPage.cs
│   ├── InventoryPage.cs
│   ├── CartPage.cs
│   └── CheckoutPage.cs
├── Tests/
│   └── SauceDemoTests.cs
├── .github/
│   └── workflows/
│       └── ci.yml
├── ExactQE.SauceDemo.csproj
└── README.md
```

## Prerequisites

- .NET 8 SDK
- Google Chrome
- Internet access

Selenium Manager is used by Selenium WebDriver to manage the browser driver, so a separate ChromeDriver executable is not required for the normal local run.

## Run locally

From the project directory:

```bash
dotnet restore
dotnet test
```

The tests run headlessly by default.

## How to run a specific test

dotnet test --filter "FullyQualifiedName~ValidLogin_ShouldOpenProductsPage"

## Design decisions

### Page Object Model

Page-specific locators and interactions are kept in page classes. Tests contain the business flow and assertions.

### Stable selectors

The solution uses IDs and `data-test` attributes where available rather than brittle XPath expressions.

### Synchronisation

Explicit waits are used for elements that require the page to be ready before interaction or assertion.

### Assertions

Assertions verify meaningful user outcomes:

- successful login reaches the Products page
- invalid login displays an authentication error
- adding products updates the cart and the expected products appear
- checkout ends with the order confirmation

### Reuse

Common login and browser setup are reused without introducing unnecessary framework abstraction.

### Failure information

Assertions include descriptive messages so a failure gives context about the expected business outcome.

## CI

A GitHub Actions workflow is included to restore, build and run the NUnit tests on every push and pull request.

### Test Diagnostics

Screenshots are captured after each test and stored as test artifacts.
Screenshots from the previous test run are removed at the start of a new
test run to avoid accumulating outdated artifacts.