# GameTwist Platform - QA Assignment

## 🔧 Assignment Task Summary

This project is a QA assignment for testing the **Login** and **Registration** functionalities of the [GameTwist](https://www.gametwist.com/en/) platform.

It includes:

- Manual Test Case Documentation
- Test Automation Recommendations
- C# API Test Automation for Swagger Petstore
- Reqnroll automation structure

---

##  Assignment Requirements

### 1. Manual Test Scenarios (Login & Registration)

- Developed 15 test cases using **Gherkin syntax** for clarity.
- Focused on both functional and validation scenarios.
- All test cases include:
  - Title
  - Preconditions
  - Test Description
  - Expected Results
  - Priority
  - Automation Decision

### 2. Automation Feasibility

####  **Test Cases Recommended for Automation**
- Login form display
- Successful/unsuccessful login
- 'Remember Me' checkbox functionality
- Registration with valid/invalid inputs
- Field-level validations
- Duplicate email check
- Password strength check

These tests are stable, repeatable, and critical for release confidence. Automating them using **C# and Selenium** ensures fast regression feedback (to be done in the future)

####  **Test Cases NOT Recommended for Automation**
- Email confirmation delivery
- Email link behavior
- IP-based geolocation redirect

These require external systems, network variability, or email inbox access. Best tested manually or via 3rd-party tools.

---

##  API Automation Task

Automated 4 endpoints from [Swagger Petstore](https://petstore.swagger.io):

- `POST /pet`
- `GET /pet/{petId}`
- `PUT /pet`
- `DELETE /pet/{petId}`
- `Post /pet` with malformed URL

###  Assertions
- Verified status codes 200 for all of them except the malformed url 400 and 404 for the Get request after deletion
- Validated that the `GET` response matches the `POST` request body
- C# implementation with `RestSharp` and `NUnit` and `Reqnroll`

---

##  Technologies Used

- Language: **C#**
- API Testing: **RestSharp**, **NUnit** **Reqnroll**
- Manual Testing: **Gherkin**, **Markdown**

---

##  Solution 
The solution is called Assignment and has 2 projects + the current readme file wich describes the project

## Project 1. GameTwistManual

### 🔹 Features

- `Login.feature` – Gherkin Login scenarios for manual GameTwist test cases
- `Registration.feature` – Gherkin Registration scenarios for manual GameTwist test cases


## Project 2. Petstore 
Features
Models
Services
Stepdefinitions

### 🔹 Features
- `PetStore.feature` – Gherkin scenarios for automating PetStore API requests

### 🔹 Models
- `Category.cs` – Model representing the `Category` object included in the Pet object
- `Pet.cs` – Model representing the main `Pet` object used in API payloads

### 🔹 Services
- `IPetStoreService.cs` – Interface for defining PetStore API operations (CRUD)
- `PetStoreService.cs` – Implementation of the PetStore API service using RestSharp

### 🔹 StepDefinitions
- `PetStoreStepDefinitions.cs` – Step definitions implementing steps from `PetStore.feature` using SpecFlow


For manual task the following test cases were written

| Test ID | Title                                                         | Preconditions                                                     | Test Description                                                        | Expected Result                                                                                                                                                                                | Priority | To Automate? | Automation Justification                                                          |
| ------- | ------------------------------------------------------------- | ----------------------------------------------------------------- | ----------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------- | ------------ | --------------------------------------------------------------------------------- |
| TC-001  | Login form appears after clicking the login button            | The user is on the GameTwist homepage                             | The user clicks the "Login" button                                      | The login form with fields "Nickname", "Password", 'Log in automatically' checkbox, "Login" button, "Forgotten your password?" link, "Register now" button, and "X" close button should appear | High     | ✅ Yes        | Stable UI behavior, part of core login flow                                       |
| TC-002  | Successful login with valid credentials                       | The user is on the login form                                     | The user enters valid credentials and logs in                           | The user should be logged in and redirected to the homepage                                                                                                                                    | High     | ✅ Yes        | Critical functionality and highly repeatable                                      |
| TC-003  | Unsuccessful login with invalid credentials                   | The user is on the login form                                     | The user enters invalid credentials and attempts to log in              | Error message "Invalid nickname/password combination" should be shown and login blocked                                                                                                        | High     | ✅ Yes        | Reliable negative test case, easily automated                                     |
| TC-004  | Login with 'Log in automatically' checkbox                    | The user is on the login form                                     | The user checks 'Log in automatically', logs in successfully            | User should stay logged in on return visits                                                                                                                                                    | Medium   | ✅ Yes        | Session persistence can be validated through cookies/local storage                |
| TC-005  | 'Forgotten your password?' link works                         | The user is on the login form                                     | The user clicks the “Forgotten your password?” link                     | The forgot password form should appear                                                                                                                                                         | Medium   | ✅ Yes        | Simple UI flow and easy to verify with URL/assertions                             |
| TC-006  | Automatically detect location                                 | The user logs in                                                  | The user logs in and is redirected based on IP                          | The user should be redirected to a localized version of the site (e.g. EN)                                                                                                                     | Low      | ❌ No         | Requires geolocation/IP spoofing, unstable in CI environments                     |
| TC-007  | Registering a new user with valid data                        | The user is on the GameTwist registration page                    | The user fills valid data, accepts terms, completes CAPTCHA and submits | Confirmation screen appears showing user's email and prompt to confirm it                                                                                                                      | High     | ✅ Yes        | Stable flow, data-driven and important for user onboarding                        |
| TC-008  | Validate user receives confirmation email                     | The user completed registration                                   | The user checks their email inbox for a confirmation email              | A confirmation email from [info@mx.gametwist.com](mailto:info@mx.gametwist.com) should be received                                                                                             | High     | ❌ No         | External system dependency (email server), best tested manually or with email API |
| TC-009  | Completing registration via confirmation email                | The user has received the confirmation email                      | The user clicks the \[Complete Registration] button in email            | User is redirected to site, sees "Your e-mail address has been confirmed. You can log in now." and is logged in automatically                                                                  | High     | ❌ No         | Complex due to inbox automation; can be partially covered with test email API     |
| TC-010  | Attempting to register without entering any data              | The user is on the registration page                              | The user clicks “Begin Adventure” without input                         | Validation messages appear for required fields and checkboxes                                                                                                                                  | Medium   | ✅ Yes        | Static validation, consistent behavior, ideal for regression                      |
| TC-011  | Attempting to register with individually missing field        | The user is on the registration page, all other fields are filled | The user omits one required field and submits the form                  | A validation message should appear and submission should be blocked                                                                                                                            | Medium   | ✅ Yes        | Great candidate for data-driven negative testing                                  |
| TC-012  | Registering with an already registered email                  | The user is on the registration page                              | The user enters an existing email with valid other data                 | Error message like "Email address already in use." should appear                                                                                                                               | High     | ✅ Yes        | Stable error response, necessary to avoid duplicate accounts                      |
| TC-013  | Registering without checking the terms and privacy policy     | The user is on the registration page                              | The user fills valid data but skips agreement checkbox                  | A warning should appear stating that agreement is mandatory                                                                                                                                    | Medium   | ✅ Yes        | Checkbox validation is stable and easy to verify                                  |
| TC-014  | Registering with a password below the minimum requirements    | The user is on the registration page                              | The user enters a weak password and submits form                        | Error appears: "Password must be at least 10 characters long and include 1 letter 1 number or special character"                                                                               | High     | ✅ Yes        | Field validation, predictable, should be part of regression                       |
| TC-015  | Trying to select a date of birth that makes the user underage | The user is on the registration page and opens the DOB selector   | The user selects a year that makes them underage                        | The application prevents selection of underage DOB                                                                                                                                             | Medium   | ✅ Yes        | Boundary testing, consistent logic, ideal for automation                          |
