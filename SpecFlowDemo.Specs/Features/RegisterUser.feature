Feature: Register a new User
    In order to register  a new User
    As member of the site
    So that they can log in to the site and use its features

@mytag
Scenario: Browse Register page
    When the user goes to the register user screen
    Then the register user view should be displayed

Scenario: On Successful registration the user should be redirected to Home Page
    Given The user has entered all the information
    When He Clicks on Register button
    Then He should be redirected to the home page

Scenario: Register should return error if username is missing
    Given The user has not entered the username
    When He clicks on Register
    Then He should be shown the error message "Username is required" "Username"

Scenario: Register should return error if password is missing
    Given The user has not entered the password but has entered a ConfirmPassword 
    When He clicks on Register
    Then He should be shown the error message "Password is required" "ConfirmPassword"

