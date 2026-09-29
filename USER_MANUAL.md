# Exact Online QE Technical Assessment – Part 3

# User Manual

## 1. Purpose

This manual explains how to install the prerequisites, obtain the project, run the tests and access test results.

## 2. Prerequisites

The following software is required:

- Git
- .NET 8 SDK
- Google Chrome

A separate ChromeDriver installation is not required because Selenium Manager handles browser-driver management.

## 3. Setup

### 3.1 Install Git

#### macOS

If Homebrew is installed:

    brew install git

Verify:

    git --version

#### Windows

Install Git from:

    https://git-scm.com/

Then verify:

    git --version

### 3.2 Install .NET 8

Install the .NET 8 SDK.

On macOS with Homebrew:

    brew install --cask dotnet-sdk

Verify:

    dotnet --version

An 8.x SDK should be available.

### 3.3 Install Google Chrome

Install Google Chrome from:

    https://www.google.com/chrome/

## 4. Get the Project

Clone the repository:

    git clone https://github.com/shilparawat512/exact-part3-saucedemo.git

Enter the project directory:

    cd exact-part3-saucedemo

Restore the project dependencies:

    dotnet restore

Build the project:

    dotnet build

## 5. Run the Tests

### Run All Tests

Run:

    dotnet test

The current test setup runs Chrome in headless mode by default.

The browser is configured with:

    --headless=new

The browser viewport is configured as:

    --window-size=1920,1080

Therefore, no Chrome window is displayed during normal test execution.

### Run an Individual Test

For example:

    dotnet test --filter "FullyQualifiedName~ValidLogin_ShouldOpenProductsPage"

Replace the test name with the test you want to execute.

## 6. Test Results and Screenshots

A screenshot is captured after every test execution.

Screenshots are stored locally in:

    TestResults/Screenshots/

The screenshot filename contains the test name and execution timestamp.

The screenshot directory is cleared at the beginning of a new test run, so screenshots from previous runs are removed.

## 7. GitHub Actions

The project includes a GitHub Actions workflow.

To view a CI run:

1. Open the GitHub repository.
2. Select **Actions**.
3. Select the required workflow run.
4. Open the run to view the test results.

The CI test command is:

    dotnet test --no-restore --logger "trx;LogFileName=test-results.trx"

## 8. Download CI Screenshots

Screenshots produced by GitHub Actions are uploaded as an artifact named:

    test-screenshots

To download them:

1. Open the repository on GitHub.
2. Select **Actions**.
3. Open the relevant workflow run.
4. Scroll to the **Artifacts** section.
5. Select **test-screenshots**.
6. Download the artifact.
7. Extract the downloaded archive.
8. Open the screenshot files.

The screenshot upload is configured to run even when the test step fails.

## 9. Troubleshooting

### Build fails

Check the installed .NET SDK:

    dotnet --version

The project requires .NET 8.

### Tests fail

Run the tests again and review the test output:

    dotnet test

Then check the corresponding screenshot under:

    TestResults/Screenshots/

For a CI failure, review the GitHub Actions test output and download the `test-screenshots` artifact.

## 10. Quick Reference

| Task | Command / Location |
|---|---|
| Clone repository | `git clone https://github.com/shilparawat512/exact-part3-saucedemo.git` |
| Enter project | `cd exact-part3-saucedemo` |
| Restore | `dotnet restore` |
| Build | `dotnet build` |
| Run all tests | `dotnet test` |
| Run one test | `dotnet test --filter "FullyQualifiedName~TestName"` |
| Local screenshots | `TestResults/Screenshots/` |
| CI | GitHub Actions |
| CI screenshot artifact | `test-screenshots` |