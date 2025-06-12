@login
Feature: Login Functionality on GameTwist Platform

# This document covers manual test scenarios for the Login flows on the GameTwist platform

Scenario: Login form appears after clicking the login button
  Given the user is on the GameTwist homepage
  When the user clicks the "Login" button
  Then the login form with fields "Nickname", "Password", "Log in automatically" checkbox, "Login" button, "Forgotten your password?" link, "Register now" button, and "X" close button should appear

Scenario: Successful login with valid credentials
  Given the user is on the login form
  When the user enters a valid nickname "validUser"
  And the user enters a valid password "validPass123"
  And the user clicks the "Login" button
  Then the user should be logged in successfully
  And redirected to the homepage
  
Scenario: Unsuccessful login with invalid credentials
  Given the user is on the login form
  When the user enters an invalid nickname "invalidUser"
  And the user enters an invalid password "wrongPass"
  And the user clicks the "Login" button
  Then an error message "Invalid nickname/password combination" should be displayed
  And the user should remain on the login form

#Note: Why can't the user login with email? Wouldn't it be more user friendly to allow both nickname and email?

Scenario: 'Log in automatically' checkbox is functional
  Given the user is on the login form
  When the user enters valid credentials
  And the user checks the "Log in automatically" checkbox
  And the user clicks the "Login" button
  Then the user should be logged in successfully
  And the login should persist on subsequent visits without manual re-entry (session remembered)

Scenario: 'Forgotten your password?' link works
  Given the user is on the login form
  When the user clicks the "Forgotten your password?" link
  Then the forgot password form should appear

Scenario: Automatically detect location
  Given the user logins
  Then the user sould be redirected to the website based on the country based on his location/IP address (e.g https://www.gametwist.com/en/)

 # Note: This scenario is basically an improvement, I noticed myself getting redirected to EN, not sure if it exists or not for other countries but for Romanian it didnt work 