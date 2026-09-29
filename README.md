# Exact Online QE Technical Assessment – Part 3

## Overview

This project contains a UI automation solution for the SauceDemo application, created as part of the Exact Online Quality Engineer Technical Assessment.

The solution demonstrates:

- UI automation using Selenium WebDriver
- C# with .NET 8
- NUnit as the test framework
- Page Object Model for maintainable test design
- Explicit waits for stable synchronization
- Positive and negative login validation
- Product selection and cart validation
- Checkout flow validation
- Screenshot capture after every test
- Headless test execution
- GitHub Actions CI execution
- Screenshot artifacts from CI runs

The implementation focuses on readable, maintainable automation without unnecessary abstraction.

## Technology Stack

| Technology | Purpose |
|---|---|
| C# | Programming language |
| .NET 8 | Runtime and project framework |
| Selenium WebDriver | Browser automation |
| NUnit | Test framework |
| NUnit3TestAdapter | NUnit integration with `dotnet test` |
| Selenium.Support | Selenium support utilities such as explicit waits |
| Google Chrome | Browser used for UI tests |
| Git | Source control |
| GitHub Actions | Continuous integration |

## Application Under Test

The tests use the SauceDemo application:

    https://www.saucedemo.com/

The automation covers:

1. Successful login
2. Invalid login
3. Adding product(s) to the cart
4. Completing checkout
5. Verifying successful order completion

## Project Structure

    ExactQE-SauceDemo/
    │
    ├── .github/
    │   └── workflows/
    │       └── ci.yml
    │
    ├── Pages/
    │   ├── LoginPage.cs
    │   ├── InventoryPage.cs
    │   ├── CartPage.cs
    │   └── CheckoutPage.cs
    │
    ├── Tests/
    │   └── SauceDemoTests.cs
    │
    ├── .gitignore
    ├── ExactQE.SauceDemo.csproj
    ├── README.md
    └── USER_MANUAL.md

Generated files such as `bin/`, `obj/`, and `TestResults/` are excluded from source control.

## Page Objects

### LoginPage

Responsible for interactions with the login screen, including entering credentials, submitting login, and exposing login error information.

### InventoryPage

Responsible for interactions with the product inventory, including adding products to the cart and navigating to the cart.

### CartPage

Responsible for cart-page interactions, including verifying selected products and starting checkout.

### CheckoutPage

Responsible for entering customer details, continuing through checkout, completing the order, and exposing the completion message or checkout error.

## Framework Design

### Page Object Model

Page-specific locators and actions are kept inside Page Objects rather than directly inside test methods. This keeps tests focused on business scenarios and makes UI changes easier to maintain.

### Synchronization

The framework uses Selenium explicit waits rather than fixed `Thread.Sleep` delays. Important page transitions and elements are waited for before the next action.

### Element Selection

Stable selectors such as `id` and `data-test` attributes are preferred where available. This reduces dependence on visual layout or DOM structure.

### Assertions

Assertions validate meaningful application outcomes, including successful navigation, login errors, products in the cart, and checkout completion.

### Test Data

The current tests use the fixed test data required by SauceDemo. Additional test-data infrastructure has not been introduced because it is unnecessary for the scope of this assessment.

### Limited Abstraction

The project intentionally avoids excessive framework layers. The goal is a small, readable and maintainable automation solution appropriate for the assessment.

## Screenshots

A screenshot is captured after every test execution.

Local screenshots are stored in:

    TestResults/Screenshots/

The screenshot directory is cleaned before a new test run so screenshots from previous executions do not accumulate.

Screenshots are also registered as NUnit test attachments.

## GitHub Actions

The repository contains:

    .github/workflows/ci.yml

The workflow runs on pushes and pull requests.

It:

1. Checks out the repository.
2. Sets up .NET 8.
3. Restores dependencies.
4. Runs the NUnit tests.
5. Uploads screenshots even if tests fail.

The CI screenshot artifact is named:

    test-screenshots

See `USER_MANUAL.md` for complete setup, execution and CI instructions.

## Generated Files and .gitignore

Generated development and test files are excluded using `.gitignore`, including:

    bin/
    obj/
    TestResults/
    Screenshots/
    .vs/
    .vscode/
    .idea/
    .DS_Store

## Quick Start

From the project root:

    dotnet restore
    dotnet build
    dotnet test

For headless execution:

    HEADLESS=true dotnet test

For an individual test:

    dotnet test --filter "FullyQualifiedName~ValidLogin_ShouldOpenProductsPage"

For complete setup instructions, see `USER_MANUAL.md`.
