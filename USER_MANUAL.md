# Exact Online QE Technical Assessment – Part 3

## Overview

This project contains a UI automation solution for the SauceDemo application, created as part of the Exact Online Quality Engineer Technical Assessment.

The solution demonstrates maintainable UI automation using Selenium WebDriver, NUnit and C#.

The implementation focuses on:

- Maintainable test structure
- Page Object Model
- Stable element selection
- Explicit synchronization
- Meaningful assertions
- Test diagnostics through screenshots
- CI execution with GitHub Actions

## Technology Stack

| Technology | Purpose |
|---|---|
| C# | Programming language |
| .NET 8 | Application framework |
| Selenium WebDriver | Browser automation |
| NUnit | Test framework |
| NUnit3TestAdapter | NUnit integration with `dotnet test` |
| Selenium.Support | Selenium wait and support functionality |
| Google Chrome | Browser |
| Git | Source control |
| GitHub Actions | CI |

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

Generated files such as `bin/`, `obj/` and `TestResults/` are excluded from source control.

## Test Coverage

The automated suite covers four scenarios:

1. Successful login
2. Invalid login
3. Adding two products to the cart
4. Completing checkout and verifying the order confirmation

The tests cover both positive and negative application behaviour.

## Page Object Model

The project uses the Page Object Model to separate page-specific interaction from test scenarios.

### LoginPage

Responsible for:

- Opening the application
- Entering username and password
- Submitting the login form
- Reading the login error

### InventoryPage

Responsible for:

- Reading the inventory page title
- Adding products
- Reading the cart count
- Opening the cart

### CartPage

Responsible for:

- Synchronizing with the cart page
- Checking whether products are present
- Starting checkout

### CheckoutPage

Responsible for:

- Entering customer details
- Continuing checkout
- Finishing the order
- Reading the completion message
- Reading checkout errors

## Framework Design Decisions

### Page Object Model

Page-specific locators and actions are kept inside Page Objects rather than directly in the tests.

This keeps the test methods focused on user behaviour and makes page changes easier to maintain.

### Synchronization

The automation uses Selenium explicit waits for important page transitions and elements.

Fixed delays such as `Thread.Sleep` are not used.

The waits synchronize the test with the application state rather than waiting for an arbitrary amount of time.

### Element Selection

The implementation prefers stable selectors such as:

- Element IDs
- `data-test` attributes

Examples:

    By.Id("checkout")

    By.CssSelector("[data-test='complete-header']")

This reduces dependency on CSS styling or DOM position.

### Assertions

Assertions validate meaningful application outcomes rather than simply checking that a Selenium action completed.

Examples include:

- Successful login opens the Products page.
- Invalid login displays the expected error.
- The expected products are present in the cart.
- The cart count is correct.
- Successful checkout displays the order confirmation.

### Test Data

The test suite uses the fixed data required by the SauceDemo scenarios.

No additional test-data framework was introduced because it would add unnecessary complexity for the scope of this assessment.

### Limited Abstraction

The framework intentionally remains lightweight.

Additional abstraction layers or custom framework components were avoided where they would not provide meaningful value for this application.

## Test Diagnostics

A screenshot is captured after every test execution.

Screenshots are registered as NUnit test attachments and are also written to the test-results directory.

The screenshot implementation provides visual evidence of the browser state for both successful and failed tests.

The screenshot directory is cleaned before a new test run to prevent old screenshots from accumulating.

## CI Design

The project contains a GitHub Actions workflow under:

    .github/workflows/ci.yml

The workflow is triggered by:

- Push
- Pull request

The CI pipeline:

1. Checks out the repository.
2. Sets up .NET 8.
3. Restores dependencies.
4. Executes the automated tests.
5. Uploads generated screenshots as an artifact.

The screenshot artifact is named:

    test-screenshots

The upload step uses `always()` so screenshots remain available when the test execution fails.

## Source Control

The `.gitignore` excludes generated and local development files, including:

    bin/
    obj/
    TestResults/
    Screenshots/
    .vs/
    .vscode/
    .idea/
    .DS_Store

## Design Summary

The solution deliberately uses a simple structure appropriate for the size and scope of the assessment:

- NUnit for test execution
- Selenium WebDriver for browser automation
- Page Objects for maintainability
- Explicit waits for synchronization
- Stable selectors for UI interaction
- NUnit assertions for behaviour validation
- Screenshots for diagnostics
- GitHub Actions for CI execution

For installation and execution instructions, see `USER_MANUAL.md`.