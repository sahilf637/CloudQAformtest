# CloudQA Practice Form Automation Test

This C# Selenium automation test demonstrates how to interact with complex web elements including:
- Elements in nested iframes
- Elements in shadow DOMs with multiple levels
- Form fields with various locator strategies

## Prerequisites

- .NET SDK (compatible with the version used in the project)
- Chrome browser
- ChromeDriver executable (matching your Chrome version)

## Project Structure

The solution consists of a single namespace `CloudQAAutomationTest` with two classes:
- `Program`: Entry point that handles execution flow and error handling
- `FormAutomationTest`: Contains the test logic and browser automation code

## Features

### Element Location Strategies

The test demonstrates multiple approaches to locate web elements:
- Multiple locator fallback using the custom `FindElementWithMultipleStrategies` method
- Navigation through nested iframes
- Traversal of deep shadow DOM structures using JavaScript

### Shadow DOM Handling

The test includes JavaScript code to traverse a 6-level deep shadow DOM structure to locate and interact with elements that cannot be accessed through standard Selenium selectors.

## Running the Tests

1. Ensure ChromeDriver is installed and available in your PATH
2. Build the project using your preferred IDE or command line
3. Run the executable

## Test Flow

1. Navigates to the CloudQA practice form
2. Locates and fills out the first name field in a nested iframe
3. Locates and fills out the email field in an iframe with ID
4. Uses JavaScript to traverse a deep shadow DOM and fill in a textarea field

## Error Handling

The test includes robust error handling with:
- Multiple element location strategies with fallbacks
- Detailed console logging
- Try-catch blocks for graceful failure
- Proper cleanup of WebDriver resources

## Maintainability

The code demonstrates best practices for Selenium automation:
- Separation of setup, test, and cleanup logic
- Clear logging of test progress
- Smart element location with fallback strategies
- Proper WebDriver resource management