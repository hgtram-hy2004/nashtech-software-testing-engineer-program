# API Testing with RestSharp and NUnit

## Overview

This project is an API automation testing framework built with **C#**, **RestSharp**, and **NUnit**.  
It is used to verify the User API endpoint by sending HTTP requests and validating response status, response data, and JSON schema.

## Technologies

- C#
- NUnit
- RestSharp
- Newtonsoft.Json
- Newtonsoft.Json.Schema
- ExtentReports

## Project Structure

```text
HoangTramHuynh
│
├── Configuration          → Stores configuration values such as API base URL and authorization token.
│
├── Core                   → Contains the main reusable components of the API testing framework.
│   ├── API                → Handles API request creation and execution using RestSharp.
│   ├── Report             → Manages Extent Report and logs API response details.
│   ├── Services           → Contains service classes that group API actions by feature.
│   └── Utilities          → Contains helper classes for configuration, JSON reading, and path handling.
│
├── DataObjects            → Contains DTO classes for request body, response body, and test data mapping.
│
└── Test                   → Contains test classes, test data, JSON schemas, helpers, and generated reports.
    ├── Schemas            → Stores JSON schema files for response validation.
    ├── TestData           → Stores JSON test data files used by test cases.
    ├── TestResults        → Stores generated Extent Report files after test execution.
    ├── BaseTest.cs        → Sets up common test objects and report logging.
    ├── UsersApiTests.cs   → Contains tests for User API methods.
    ├── UserSchemaTests.cs → Contains the JSON schema validation test.
    └── UserTestHelper.cs  → Supports test preconditions and user test data preparation.
```
## Test Coverage
This project covers API automation tests for the User API endpoint.
### 1: Verify User API Methods

The following HTTP methods are covered:

| Test Case                                  | Method | Coverage                                                      |
| ------------------------------------------ | ------ | ------------------------------------------------------------- |
| `GetUsersSuccessfully`                     | GET    | Verify that the list of users is retrieved successfully       |
| `GetUserByIdSuccessfully`                  | GET    | Verify that a specific user can be retrieved by valid user id |
| `CreateUserSuccessfully`                   | POST   | Verify that a new user can be created successfully            |
| `UpdateUserSuccessfully`                   | PUT    | Verify that an existing user can be updated successfully      |
| `DeleteUserSuccessfully`                   | DELETE | Verify that an existing user can be deleted successfully      |

For the `Get Users` test, the validation includes:
* Status code is correct
* Response body is not empty
* User information contains the keys: `id`, `name`, `email`, `gender`, `status`

### 2: Verify JSON Schema
| Test Case                   | Method | Coverage                                                                     |
| ----------------------------| ------ | -----------------------------------------------------------------------------|
| `UpdateUserReturnJsonSchema`| PUT    | Validate schema of update user response                                      |


## How to Run Tests

### Prerequisites

Before running the project, make sure the following tools are installed:

* .NET SDK
* Visual Studio Code or Visual Studio
* Required NuGet packages restored successfully

### Configuration
Update the configuration file before running tests:
```text
Configuration/appsettings.json
```
Example:
```json
{
  "application": {
    "url": "https://gorest.co.in",
    "token": "your_token_here"
  }
}
```
The `url` is the base API URL.
The `token` is required for authorized requests such as create, update, and delete user.
Do not commit a real token to the repository.

### Restore Packages
Run this command to restore all NuGet packages:

```bash
dotnet restore
```
### Build Project
Run this command to build the project:

```bash
dotnet build
```
### Run All Tests

Run this command to execute all test cases:

```bash
dotnet test
```
### Run Tests by Category

Run Exercise 1 tests:
```bash
dotnet test --filter "Category=Users API"
```

Run Exercise 2 tests:
```bash
dotnet test --filter "Category=Users API Schemas"
```
## Test Report

This project uses ExtentReports to generate an HTML test report.
After running the tests, the report will be generated in:
```text
Test/TestResults/ExtentReports
```
Open the generated `.html` file in a browser to view the report.

The report includes:
* Test case name
* Test status: pass or fail
* API name
* Status code
* Response time
* Response body displayed in table format
* Error message and stack trace if the test fails

