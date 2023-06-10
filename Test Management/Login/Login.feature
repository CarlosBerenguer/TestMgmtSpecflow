Feature: Login
	As a user
	I wish to login the application
	to access the functionalities


@tag1
Scenario: Login Successfully
	Given the user accessed the login page
	When The user filled the login form
	And clicked on Login button
	Then The user is redirected to the main page

Scenario: Login Failed
	Given the user accessed the login page
	When The user filled the login form with wrong information
	And clicked on Login button
	Then The invalid credentials message is displayed 

Scenario: Login withou password
	Given the user accessed the login page
	When The user filled the login form without password
	And clicked on Login button
	Then The password required message is displayed 