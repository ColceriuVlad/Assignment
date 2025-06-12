@registration
Feature: Registration functinoality on GameTwist Platform

# This document covers manual test scenarios for the Registration flows on the GameTwist platform. 
  
Scenario: Registering a new user with valid data
  Given I am on the GameTwist registration page "https://www.gametwist.com/en/registration/"
  When I enter a valid email, nickname, password and date of birth (vlad.colceriu@yahoo.com, vlad233332, sFHVx_awqerqwerq3st4, 09-07-1996)
  And I accept "I'm not a robot"
  And I "I agree with the GTC and data protection guidelines."
  And I click "Begin Adventure"
  Then I should see a confirmation screen that states I need to confirm my email address
  And my email is displayed correctly on the screen

Scenario: Validate user receives confirmation email
  Given I registered correctly as previously described
  When I go to my email inbox
  Then I sould have a confirmation email from GameTwist (info@mx.gametwist.com) that corresponds to all the design requirements

Scenario: Completing registration via confirmation email
  Given I have received the confirmation email
  When I click the [Complete Registration] button in the email
  Then I should be redirected to the GameTwist website with a message confirming my registration is complete "Your e-mail address has been confirmed. You can log in now."
  Then I sould also be logged in automatically

  # Note, in this scenario the message doesnt make sense, if I'm already logged in, why does the message say I can log in now?
  # This can be an improvement for the future, doesn't impact functionality but it misleads the user
  # Might be a bug, I clicked on the register button once and it redirected me to the same page with no registration, doesn't happen always
 
 Scenario: Attempting to register without entering any data
  Given I am on the registration page
  When I click the "Begin Adventure" button without entering any values
  Then I should see validation messages for email, nickname, password, and date of birth
  And The security [Checkboxes] sould be mdatory and I should not be able to proceed without completing them

  # Note: Souldn't the Begin Adventure button be disabled untill everything is populated? Feels more user friendly'

 Scenario: Attempting to register with individually missing field
  Given I am on the registration page
  And I populate all the fields and click all the checkboxes except the "Email/Nickname/Password" field individually
  Then I sould receive a validation message for the missing field 
  And I should not be able to proceed with the registration by clicking the "Begin Adventure" button

Scenario: Registering with an already registered email
  Given I am on the registration page
  When I enter email "vlad.colceriu@yahoo.com"
  And I enter a new nickname and valid password and birth date
  And I accept terms and reCAPTCHA
  And I click "Begin Adventure"
  Then I should see an error message like "Email address already in use."

Scenario: Registering without checking the terms and privacy policy
  Given I am on the registration page
  When I fill in all valid fields
  And I skip checking "I agree with the GTC and data protection guidelines."
  And I click "Begin Adventure"
  Then I should see a warning stating that accepting the terms is mandatory

Scenario: Registering with a password below the minimum requirements
  Given I am on the registration page
  When I enter a valid email and nickname
  And I enter a password "123"
  And I provide a valid date of birth
  And I check all required checkboxes
  And I click "Begin Adventure"
  Then I should see an error saying "Password must be at least 10 characters long and include 1 letter 1 number or a special character"

Scenario: Trying to select a date of birth that makes the user underage
  Given I am on the registration page
  And I open the year of from the date of birth
  Then The application sould not allow me to select a year that makes me underage