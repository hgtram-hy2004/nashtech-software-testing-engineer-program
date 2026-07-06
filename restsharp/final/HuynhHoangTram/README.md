# API Testing with RestSharp and NUnit

## Overview

This project is an API automation testing framework built with **C#**, **RestSharp**, and **NUnit**.

It is used to verify the DemoQA Account and Book Store API endpoints by sending HTTP requests and validating response status code, response body, response data, JSON schema, and the actual data state after API actions.

The project also uses **ExtentReports** to generate an HTML report for test execution results.

## Technologies

* C#
* .NET
* NUnit
* RestSharp
* Newtonsoft.Json
* Newtonsoft.Json.Schema
* ExtentReports

## Project Structure

```text
HuynhHoangTram
│
├── Configuration
│   └── appsettings.json
│       → Stores configuration values such as API base URL.
│
├── Core
│   → Contains reusable framework components that can be reused in other API testing projects.
│
│   ├── API
│   │   └── APIClient.cs
│   │       → Handles API request creation and execution using RestSharp.
│
│   ├── Assertions
│   │   ├── ApiAssertions.cs
│   │   │   → Contains common API assertions such as status code and response content validation.
│   │   │
│   │   └── JsonSchemaAssertions.cs
│   │       → Validates response body against JSON schema files.
│
│   ├── Report
│   │   ├── ExtentReport.cs
│   │   │   → Initializes and manages Extent Report.
│   │   │
│   │   └── ReportLog.cs
│   │       → Logs API response details, response body, book list, and schema validation result.
│
│   └── Utilities
│       ├── ConfigUtils.cs
│       │   → Reads configuration values from appsettings.json.
│       │
│       ├── JsonUtils.cs
│       │   → Reads and deserializes JSON test data files.
│       │
│       └── PathUtils.cs
│           → Handles project paths, test data paths, schema paths, and report paths.
│
├── Services
│   → Contains service classes that group API actions by feature.
│
│   ├── AccountService.cs
│   │   → Handles Account API actions such as generate token and get user.
│   │
│   └── BookStoreService.cs
│       → Handles Book Store API actions such as add, delete, and replace book.
│
├── Models
│   → Contains request and response models used for API serialization and deserialization.
│
│   ├── Request
│   │   ├── AccountRequest.cs
│   │   └── BookStoreRequest.cs
│   │
│   └── Response
│       ├── AccountResponse.cs
│       ├── BookStoreResponse.cs
│       ├── ErrorResponse.cs
│       └── GenerateTokenResponse.cs
│
├── DataObject
│   → Contains DTO classes used to map test data from JSON files.
│
│   ├── AccountDto.cs
│   └── BookStoreDto.cs
│
└── Test
    → Contains test classes, test-specific assertions, test data, and JSON schemas.
│
    ├── Assertions
    │   └── BookStoreAssertions.cs
    │       → Contains project-specific assertions for Account and Book Store responses.
│
    ├── Data
    │   ├── userdata.json
    │   │   → Stores account test data, expected books, and invalid data.
    │   │
    │   ├── bookstoredata.json
    │   │   → Stores book data for add, delete, and replace book test cases.
    │   │
    │   └── Schemas
    │       └── replacebookschema.json
    │           → Stores JSON schema for Replace Book response validation.
│
    ├── BaseTest.cs
    │   → Sets up common test objects, services, token, test data, and Extent Report.
│
    ├── AccountTest.cs
    │   → Contains tests for Account API.
│
    ├── AddBookTest.cs
    │   → Contains tests for adding books to collection.
│
    ├── DeleteBookTest.cs
    │   → Contains tests for deleting books from collection.
│
    └── ReplaceBookTest.cs
        → Contains tests for replacing books in collection and validating JSON schema.
```

## Test Coverage

This project covers API automation tests for the DemoQA Account and Book Store APIs.

### 1. Account API

The Account API test covers the `GET /Account/v1/User/{userId}` endpoint. The success test verifies that the user information can be retrieved with a valid user id and valid token. It validates the status code, response body, `userId`, `username`, and the books in the user's collection if available.

The negative test verifies that the API returns an error when getting user information with an invalid user id.

### 2. Add Book API

The Add Book API test covers the `POST /BookStore/v1/Books` endpoint. The success test verifies that a book can be added to the user's collection successfully. After adding the book, the test calls the Get User API again to confirm that the added book really exists in the user's collection.

The negative tests verify that the API returns an error when adding a book with an invalid ISBN, and that the API does not allow adding a book that already exists in the user's collection.

### 3. Delete Book API

The Delete Book API test covers the `DELETE /BookStore/v1/Book` endpoint. The success test verifies that a book can be deleted from the user's collection successfully. After deleting the book, the test calls the Get User API again to confirm that the deleted book no longer exists in the user's collection.

The negative tests verify that the API returns an error when deleting a book with an invalid ISBN, and that the API does not allow deleting a book with an invalid token.

### 4. Replace Book API

The Replace Book API test covers the `PUT /BookStore/v1/Books/{isbn}` endpoint. 
The success test verifies that a book can be replaced in the user's collection successfully. It validates the status code, response body, JSON schema, and the new ISBN in the response.

The negative tests verify that the API returns an error when replacing a book with an invalid ISBN, and that the API does not allow replacing a book with an invalid token.

### bookstoredata.json

This file contains book data for Add Book, Delete Book, and Replace Book test cases.

Example:

```json
{
  "addBook": {
    "isbn": "9781491904244",
    "duplicateIsbn": "9781449325862",
    "invalidIsbn": "0000000000000"
  },
  "deleteBook": {
    "isbn": "9781593277574",
    "invalidIsbn": "1111111111111"
  },
  "replaceBook": {
    "oldIsbn": "9781449331818",
    "newIsbn": "9781593275846",
    "invalidIsbn": "0000000000000"
  }
}
```

## JSON Schema Validation

This project uses JSON schema validation for the Replace Book success test.

Schema file location:

```text
Test/Data/Schemas/replacebookschema.json
```

The validation checks whether the response body structure matches the expected schema.

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
    "url": "https://demoqa.com"
  }
}
```

The `url` is the base API URL.

Account username and password are stored in `userdata.json` and are used to generate a token before running the tests.

Do not commit real account credentials to the repository.

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

Run Account API tests:

```bash
dotnet test --filter "TestCategory=AccountAPI"
```

Run Add Book API tests:

```bash
dotnet test --filter "TestCategory=AddBookAPI"
```

Run Delete Book API tests:

```bash
dotnet test --filter "TestCategory=DeleteBookAPI"
```

Run Replace Book API tests:

```bash
dotnet test --filter "TestCategory=ReplaceBookAPI"
```

## Test Report

This project uses ExtentReports to generate an HTML test report.

After running the tests, the report will be generated in:

```text
bin/Debug/net10.0/TestResults/ExtentReports
```

Open the generated `.html` file in a browser to view the report.

The report includes:

* Test case name
* Test status: pass or fail
* API name
* Status code
* Response time
* Response body displayed in table format
* Book collection displayed with ISBN and title
* JSON schema validation result
* Error message and stack trace if the test fails

